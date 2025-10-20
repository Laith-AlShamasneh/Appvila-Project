// ===============================================
// main.js
// Shared API utility for Scholar / .NET 8 API
// Using jQuery + SweetAlert2 + modern loader
// ===============================================

// Ensure dependencies exist
if (typeof $ === "undefined") {
  throw new Error("jQuery is required for apiHelper.js");
}
if (typeof Swal === "undefined") {
  throw new Error("SweetAlert2 is required for apiHelper.js");
}

// ===============================================
//  CONFIG
// ===============================================
const ApiHelper = (function () {
  // Base API endpoint (adjust if needed)
  const BASE_URL = "http://localhost:53578/api";

  // ===============================================
  //  Loader Handling – use template preloader
  // ===============================================
  function showLoader() {
    const $preloader = $("#js-preloader");
    if ($preloader.length) {
      $preloader.addClass("show").fadeIn(200);
    }
  }

  function hideLoader() {
    const $preloader = $("#js-preloader");
    if ($preloader.length) {
      setTimeout(
        () => $preloader.fadeOut(300, () => $preloader.removeClass("show")),
        200
      );
    }
  }

  // ===============================================
  //  SweetAlert2 Wrapper
  // ===============================================
  function showMessage(type, title, text) {
    const config = {
      title: title || "",
      text: text || "",
      confirmButtonColor: "#3085d6",
    };

    switch (type) {
      case "success":
        Swal.fire({ icon: "success", ...config });
        break;
      case "error":
        Swal.fire({ icon: "error", ...config });
        break;
      case "warning":
        Swal.fire({ icon: "warning", ...config });
        break;
      case "info":
        Swal.fire({ icon: "info", ...config });
        break;
      default:
        Swal.fire(config);
    }
  }

  // ===============================================
  //  Response Handler (Based on StandardApiResponse)
  // ===============================================
  function handleApiResponse(response, onSuccess, onError) {
    if (!response) {
      showMessage("error", "Empty Response", "Server returned no data.");
      if (onError) onError("Empty response");
      return;
    }

    const { code, success, message, data } = response;

    switch (code) {
      case 1: // OK
      case 2: // Created
      case 3: // Accepted
      case 4: // Found
        if (success) {
          if (onSuccess) onSuccess(data, message);
        } else {
          showMessage("error", "Failed", message || "Unknown error");
          if (onError) onError(message);
        }
        break;
      case 5:
        showMessage("error", "Bad Request", message);
        break;
      case 6:
        showMessage("error", "Unauthorized", "Please log in again.");
        break;
      case 7:
        showMessage("error", "Forbidden", "You don’t have permission.");
        break;
      case 8:
        showMessage("warning", "Data Not Found", "No data available.");
        break;
      case 9:
        showMessage("error", "Method Not Allowed", "Invalid request method.");
        break;
      case 10:
        showMessage(
          "error",
          "Server Error",
          "An internal server error occurred."
        );
        break;
      case 11:
        showMessage("warning", "Timeout", "The request took too long.");
        break;
      default:
        showMessage("error", "Unknown Code", `Response Code: ${code}`);
        break;
    }
  }

  // ===============================================
  //  Unified POST API call
  // ===============================================
  function callApi(
    endpoint,
    data = {},
    onSuccess = null,
    onError = null,
    options = {}
  ) {
    const fullUrl = BASE_URL + endpoint;

    $.ajax({
      url: fullUrl,
      type: "POST", // all calls are POST
      data: JSON.stringify(data),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      headers: options.headers || {}, // add token here if needed
      timeout: options.timeout || 20000,

      beforeSend: function () {
        if (options.showLoader !== false) showLoader();
      },

      complete: function () {
        hideLoader();
      },

      success: function (response) {
        handleApiResponse(response, onSuccess, onError);
      },

      error: function (xhr, status, error) {
        hideLoader();
        showMessage("error", "Network Error", error || "Unexpected error");
        if (onError) onError(error);
      },
    });
  }

  // ===============================================
  //  Public API
  // ===============================================
  return {
    callApi,
    showMessage,
    showLoader,
    hideLoader,
  };
})();
