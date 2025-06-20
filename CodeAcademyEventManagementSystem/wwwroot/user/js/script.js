// Navigation functionality
function showSection(sectionId) {
  // Hide all sections
  const sections = document.querySelectorAll(".content-section")
  sections.forEach((section) => {
    section.classList.remove("active")
  })

  // Show selected section
  const targetSection = document.getElementById(sectionId)
  if (targetSection) {
    targetSection.classList.add("active")
  }

  // Update active menu item
  const menuItems = document.querySelectorAll(".sidebar-menu a")
  menuItems.forEach((item) => {
    item.classList.remove("active")
  })

  // Find and activate the clicked menu item
  const activeMenuItem = document.querySelector(`[data-section="${sectionId}"]`)
  if (activeMenuItem) {
    activeMenuItem.classList.add("active")
  }

  // Close mobile sidebar
  if (window.innerWidth <= 768) {
    const sidebar = document.getElementById("sidebar")
    const overlay = document.querySelector(".sidebar-overlay")
    if (sidebar) {
      sidebar.classList.remove("show")
    }
    if (overlay) {
      overlay.classList.remove("show")
    }
  }
}

// Toggle sidebar for mobile
function toggleSidebar() {
  const sidebar = document.getElementById("sidebar")
  const overlay = document.querySelector(".sidebar-overlay")

  if (sidebar && overlay) {
    sidebar.classList.toggle("show")
    overlay.classList.toggle("show")
  }
}

// Close sidebar when clicking outside on mobile
document.addEventListener("click", (event) => {
  const sidebar = document.getElementById("sidebar")
  const toggle = document.querySelector(".mobile-toggle")
  const overlay = document.querySelector(".sidebar-overlay")

  if (window.innerWidth <= 768 && sidebar && toggle && overlay) {
    if (!sidebar.contains(event.target) && !toggle.contains(event.target)) {
      sidebar.classList.remove("show")
      overlay.classList.remove("show")
    }
  }
})

// Handle window resize
window.addEventListener("resize", () => {
  const sidebar = document.getElementById("sidebar")
  const overlay = document.querySelector(".sidebar-overlay")

  if (window.innerWidth > 768 && sidebar && overlay) {
    sidebar.classList.remove("show")
    overlay.classList.remove("show")
  }
})

// Add loading animation to buttons
document.addEventListener("DOMContentLoaded", () => {
  const buttons = document.querySelectorAll(".btn")
  buttons.forEach((button) => {
    button.addEventListener("click", function (e) {
      if (!this.classList.contains("no-loading")) {
        const originalText = this.innerHTML
        this.innerHTML = '<span class="loading"></span> Loading...'

        setTimeout(() => {
          this.innerHTML = originalText
        }, 1000)
      }
    })
  })

  // Add smooth hover effects to sidebar menu items
  const menuItems = document.querySelectorAll(".sidebar-menu a")
  menuItems.forEach((item) => {
    item.addEventListener("mouseenter", function () {
      if (!this.classList.contains("active")) {
        this.style.transform = "translateX(5px)"
      }
    })

    item.addEventListener("mouseleave", function () {
      if (!this.classList.contains("active")) {
        this.style.transform = "translateX(0)"
      }
    })
  })

  // Initialize with home section active
  showSection("home")
})

// Smooth scrolling for better UX
document.querySelectorAll('a[href^="#"]').forEach((anchor) => {
  anchor.addEventListener("click", function (e) {
    e.preventDefault()
    const target = document.querySelector(this.getAttribute("href"))
    if (target) {
      target.scrollIntoView({
        behavior: "smooth",
        block: "start",
      })
    }
  })
})

// Add keyboard navigation support
document.addEventListener("keydown", (e) => {
  // ESC key closes mobile sidebar
  if (e.key === "Escape") {
    const sidebar = document.getElementById("sidebar")
    const overlay = document.querySelector(".sidebar-overlay")
    if (sidebar && overlay && sidebar.classList.contains("show")) {
      sidebar.classList.remove("show")
      overlay.classList.remove("show")
    }
  }
})
