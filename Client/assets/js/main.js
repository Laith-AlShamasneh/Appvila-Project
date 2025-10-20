(function () {
  //===== Preloader
  window.onload = function () {
    window.setTimeout(fadeout, 500);
  };

  function fadeout() {
    const preloader = document.querySelector(".preloader");
    if (!preloader) return;
    preloader.style.opacity = "0";
    preloader.style.display = "none";
  }

  /*=====================================
    Sticky Navbar + Back to Top
  =======================================*/
  window.onscroll = function () {
    const header_navbar = document.querySelector(".navbar-area");
    const logo = document.querySelector(".navbar-brand img");
    const backToTop = document.querySelector(".scroll-top");

    if (!header_navbar) return;

    const sticky = header_navbar.offsetTop;

    if (window.pageYOffset > sticky) {
      header_navbar.classList.add("sticky");
      if (logo)
        logo.src = websiteLogos.headerLogoUrl || "assets/images/logo/logo.svg";
    } else {
      header_navbar.classList.remove("sticky");
      if (logo)
        logo.src =
          websiteLogos.footerLogoUrl || "assets/images/logo/white-logo.svg";
    }

    if (backToTop) {
      if (
        document.body.scrollTop > 50 ||
        document.documentElement.scrollTop > 50
      ) {
        backToTop.style.display = "flex";
      } else {
        backToTop.style.display = "none";
      }
    }
  };

  /*=====================================
    Section Scroll Active Menu
  =======================================*/
  function onScroll() {
    const links = document.querySelectorAll(".page-scroll");
    const scrollPos =
      window.pageYOffset ||
      document.documentElement.scrollTop ||
      document.body.scrollTop;

    links.forEach((link) => {
      const val = link.getAttribute("href");
      if (!val || !val.startsWith("#")) return; // skip external or invalid links

      const refElement = document.querySelector(val);
      if (!refElement) return; // skip if the section doesn’t exist

      const scrollTopMinus = scrollPos + 73;

      if (
        refElement.offsetTop <= scrollTopMinus &&
        refElement.offsetTop + refElement.offsetHeight > scrollTopMinus
      ) {
        links.forEach((l) => l.classList.remove("active"));
        link.classList.add("active");
      } else {
        link.classList.remove("active");
      }
    });
  }

  document.addEventListener("scroll", onScroll);

  // Smooth scrolling for menu links
  const pageLinks = document.querySelectorAll(".page-scroll");
  pageLinks.forEach((elem) => {
    elem.addEventListener("click", (e) => {
      e.preventDefault();
      const target = document.querySelector(elem.getAttribute("href"));
      if (!target) return;

      target.scrollIntoView({
        behavior: "smooth",
        block: "start",
      });
    });
  });

  /*=====================================
    WOW.js Animations
  =======================================*/
  if (typeof WOW === "function") {
    new WOW().init();
  } else {
    console.warn("WOW.js not found — skipping animations init.");
  }

  /*=====================================
    Portfolio Filter Buttons
  =======================================*/
  const filterButtons = document.querySelectorAll(
    ".portfolio-btn-wrapper button"
  );
  filterButtons.forEach((btn) => {
    btn.addEventListener("click", (event) => {
      const filterValue = event.target.getAttribute("data-filter");
      if (typeof iso !== "undefined" && iso.arrange) {
        iso.arrange({ filter: filterValue });
      }
    });
  });

  const elements = document.getElementsByClassName("portfolio-btn");
  for (let i = 0; i < elements.length; i++) {
    elements[i].onclick = function () {
      for (let j = 0; j < elements.length; j++) {
        elements[j].classList.remove("active");
      }
      this.classList.add("active");
    };
  }

  /*=====================================
    Mobile Menu Toggle
  =======================================*/
  const navbarToggler = document.querySelector(".mobile-menu-btn");
  if (navbarToggler) {
    navbarToggler.addEventListener("click", function () {
      navbarToggler.classList.toggle("active");
    });
  }

  /*=====================================
    Mutation Observer (for dynamically loaded sections)
  =======================================*/
  // Rebind scroll tracking when new content (like About Us or Features) is loaded dynamically
  const observer = new MutationObserver(() => {
    // Re-run scroll binding in case new sections were added
    document.removeEventListener("scroll", onScroll);
    document.addEventListener("scroll", onScroll);
  });

  observer.observe(document.body, { childList: true, subtree: true });
})();
