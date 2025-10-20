let websiteLogos = {};

// -----------------------------
// Initialize App
// -----------------------------
$(document).ready(function () {
  loadHeaderMenu();
  loadAboutUs();
  loadFeatures();
  loadAchievements();
  loadPricing();
  loadCallAction();
  loadFooterMenus();
  loadSettings();
});

// -----------------------------
// Function: Load Header Menu
// -----------------------------
function loadHeaderMenu() {
  ApiHelper.callApi(
    "/home/get/header-menu",
    {},
    function (data) {
      renderHeaderMenu(data);
    },
    function (error) {
      alert("Header menu failed:", error);
    },
    {
      headers: {
        "Accept-Language": "1",
      },
      showLoader: true,
    }
  );
}

// -----------------------------
// Function: Render Menu
// -----------------------------
function renderHeaderMenu(menuItems) {
  if (!Array.isArray(menuItems)) {
    console.warn("Invalid menu data format:", menuItems);
    return;
  }

  const $menu = $(".navbar-nav.ms-auto");
  if (!$menu.length) return;

  $menu.empty();
  menuItems.sort((a, b) => a.order - b.order);

  menuItems.forEach((item, index) => {
    const activeClass = index === 0 ? "active" : "";
    const li = `
      <li class="nav-item">
        <a href="${item.linkUrl || "javascript:void(0)"}" 
           class="page-scroll ${activeClass}" 
           ${item.tooltip ? `title="${item.tooltip}"` : ""}>
          ${item.title || "Untitled"}
        </a>
      </li>
    `;
    $menu.append(li);
  });

  $menu.find("a").on("click", function () {
    $menu.find("a").removeClass("active");
    $(this).addClass("active");
  });
}

// -----------------------------
// Function: Load About Us Section
// -----------------------------
function loadAboutUs() {
  ApiHelper.callApi(
    "/home/get/about-us",
    {},
    function (data) {
      renderAboutUs(data);
    },
    function (error) {
      console.error("Failed to load About Us:", error);
    },
    {
      headers: {
        "Accept-Language": "1",
      },
      showLoader: false,
    }
  );
}

// -----------------------------
// Function: Render About Us Section
// -----------------------------
function renderAboutUs(data) {
  if (!data || typeof data !== "object") {
    console.warn("Invalid About Us data format:", data);
    return;
  }

  // Elements
  const $title = $("#about-title");
  const $description = $("#about-description");
  const $buttons = $("#about-buttons");
  const $image = $("#about-image");

  // Title & Description
  $title.text(data.title || "");
  $description.text(data.description || "");

  // Clear buttons before re-rendering
  $buttons.empty();

  // App Store Button
  if (data.hasAppStoreButton) {
    const icon = data.appStoreButtonIconClass || "lni lni-apple";
    const text = data.appStoreButtonText || "App Store";
    const link = data.appStoreButtonLinkUrl || "javascript:void(0)";
    $buttons.append(`
      <a href="${link}" class="btn">
        <i class="${icon}"></i> ${text}
      </a>
    `);
  }

  // Google Play Button
  if (data.hasGooglePlayButton) {
    const icon = data.googlePlayButtonIconClass || "lni lni-play-store";
    const text = data.googlePlayButtonText || "Google Play";
    const link = data.googlePlayButtonLinkUrl || "javascript:void(0)";
    $buttons.append(`
      <a href="${link}" class="btn btn-alt">
        <i class="${icon}"></i> ${text}
      </a>
    `);
  }

  // Image
  const imageUrl = data.imageUrl || "assets/images/hero/default.png";
  $image.attr("src", imageUrl);
}

// -----------------------------
// Function: Load Features
// -----------------------------
function loadFeatures() {
  ApiHelper.callApi(
    "/home/get/features",
    {},
    function (data) {
      renderFeatures(data);
    },
    function (error) {
      console.error("Failed to load features:", error);
    },
    {
      headers: {
        "Accept-Language": "1",
      },
      showLoader: false,
    }
  );
}

// -----------------------------
// Function: Render Features
// -----------------------------
function renderFeatures(featureData) {
  if (!featureData || !Array.isArray(featureData.items)) {
    console.warn("Invalid feature data:", featureData);
    return;
  }

  // Set section title & description
  $("#features-subtitle").text(featureData.title || "Features");
  $("#features-title").text(featureData.brief || "");
  $("#features-description").text(featureData.description || "");

  // Target container
  const $container = $("#features-container");
  $container.empty();

  // Loop through feature items
  featureData.items.forEach((item, index) => {
    const icon = item.iconClass || "lni lni-star";
    const delay = 0.2 + (index % 3) * 0.2; // stagger animation delay

    const featureHtml = `
      <div class="col-lg-4 col-md-6 col-12">
        <div class="single-feature wow fadeInUp" data-wow-delay="${delay}s">
          <i class="${icon}"></i>
          <h3>${item.title || "Untitled"}</h3>
          <p>${item.description || ""}</p>
        </div>
      </div>
    `;

    $container.append(featureHtml);
  });
}

// -----------------------------
// Function: Load Achievements
// -----------------------------
function loadAchievements() {
  ApiHelper.callApi(
    "/home/get/achievement",
    {},
    function (data) {
      renderAchievements(data);
    },
    function (error) {
      console.error("Failed to load achievements:", error);
    },
    {
      headers: { "Accept-Language": "1" },
      showLoader: true,
    }
  );
}

// -----------------------------
// Function: Render Achievements
// -----------------------------
function renderAchievements(data) {
  if (!data) {
    console.warn("Invalid achievements data:", data);
    return;
  }

  // Set title and description
  $("#achievement-title").text(data.title || "Achievements");
  $("#achievement-description").text(data.description || "");

  // Set counters (keep labels fixed)
  $("#achievement-satisfaction").text(data.satisfactionValue || "0");
  $("#achievement-happy-users").text(data.hapyUserValue || "0");
  $("#achievement-downloads").text(data.downloadValue || "0");
}

function loadPricing() {
  ApiHelper.callApi(
    "/home/get/pricing-plans",
    {},
    function (data) {
      renderPricing(data);
    },
    function (error) {
      console.error("Failed to load pricing section:", error);
    },
    {
      headers: { "Accept-Language": "1" },
      showLoader: true,
    }
  );
}

function renderPricing(pricing) {
  const section = $("#pricing");
  section.find(".section-title h3").text(pricing.title);
  section.find(".section-title h2").text(pricing.brief);
  section.find(".section-title p").text(pricing.description || "");

  const container = $("#pricing-cards-container");
  container.empty();

  if (!pricing.items || pricing.items.length === 0) {
    container.html("<p class='text-center'>No pricing plans available.</p>");
    return;
  }

  $.each(pricing.items, function (index, item) {
    const delay = (index + 1) * 0.2;
    const labelHtml = item.labelText ? item.labelText : "";

    const featuresHtml = $.map(item.features, function (f) {
      return `<li><i class="lni lni-checkmark-circle"></i> ${f.title}</li>`;
    }).join("");

    const cardHtml = `
      <div class="col-lg-3 col-md-6 col-12">
        <div class="single-table wow fadeInUp" data-wow-delay="${delay}s">
          <div class="table-head">
            <h4 class="title">${item.title}</h4>
            <p>${item.description || ""}</p>
            <div class="price">
              <h2 class="amount">$${
                item.pricePerMonth
              }<span class="duration">/mo</span></h2>
            </div>
            <div class="button">
              <a href="javascript:void(0)" class="btn">${item.buttonText}</a>
            </div>
          </div>
          <div class="table-content">
            <h4 class="middle-title">${labelHtml}</h4>
            <ul class="table-list">
              ${featuresHtml}
            </ul>
          </div>
        </div>
      </div>
    `;

    container.append(cardHtml);
  });
}

function loadCallAction() {
  ApiHelper.callApi(
    "/home/get/call-action",
    {},
    function (data) {
      renderCallAction(data);
    },
    function (error) {
      console.error("Failed to load call action section:", error);
    },
    {
      headers: { "Accept-Language": "1" },
      showLoader: true,
    }
  );
}

function renderCallAction(callAction) {
  const section = $("#call-action");
  const titleEl = section.find(".cta-content h2");
  const descEl = section.find(".cta-content p");
  const buttonWrapper = section.find(".cta-content .button");
  const button = buttonWrapper.find(".btn");

  titleEl.text(callAction.title);
  descEl.text(callAction.description);

  if (callAction.hasButton && callAction.buttonText) {
    button.text(callAction.buttonText);
    buttonWrapper.show();
  } else {
    buttonWrapper.hide();
  }
}

function loadFooterMenus() {
  ApiHelper.callApi(
    "/home/get/footer-menus",
    {},
    function (data) {
      renderFooterMenus(data);
    },
    function (error) {
      console.error("Failed to load footer menus:", error);
    },
    {
      headers: { "Accept-Language": "1" },
      showLoader: true,
    }
  );
}

function renderFooterMenus(data) {
  renderFooterList("#footer-solutions", data.solutions);
  renderFooterList("#footer-support", data.support);
  renderFooterList("#footer-company", data.company);
  renderFooterList("#footer-legal", data.legal);
}

function renderFooterList(selector, items) {
  const $ul = $(selector);
  $ul.empty();

  if (!items || !items.length) {
    $ul.append("<li><a href='javascript:void(0)'>No items</a></li>");
    return;
  }

  $.each(items, function (_, item) {
    const li = `<li><a href="${item.linkUrl || "javascript:void(0)"}">${
      item.text
    }</a></li>`;
    $ul.append(li);
  });
}

function loadSettings() {
  ApiHelper.callApi(
    "/home/get/setting",
    {},
    function (data) {
      renderSettings(data);
    },
    function (error) {
      console.error("Failed to load settings:", error);
    },
    {
      headers: { "Accept-Language": "1" },
      showLoader: false,
    }
  );
}

function renderSettings(data) {
  if (!data) return;

  websiteLogos = data;

  // Header logo
  if (data.headerLogoUrl) $("#main-logo").attr("src", data.footerLogoUrl);

  // Footer logo
  if (data.footerLogoUrl) $("#footer-logo").attr("src", data.footerLogoUrl);

  // Footer description
  if (data.footerDescription)
    $("#footer-description").text(data.footerDescription);

  // Designed by
  if (data.designedBy) $("#footer-designedby").html(data.designedBy);
}
