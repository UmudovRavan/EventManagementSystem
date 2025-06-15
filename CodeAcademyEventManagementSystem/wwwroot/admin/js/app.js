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
  event.target.classList.add("active")

  // Close mobile sidebar
  if (window.innerWidth <= 768) {
    document.getElementById("sidebar").classList.remove("show")
  }
}

function showDashboard() {
  showSection("dashboard")

  // Reset all menu items and activate dashboard
  const menuItems = document.querySelectorAll(".sidebar-menu a")
  menuItems.forEach((item) => {
    item.classList.remove("active")
  })
  document.querySelector('.sidebar-menu a[onclick*="dashboard"]').classList.add("active")
}

function toggleSidebar() {
  const sidebar = document.getElementById("sidebar")
  sidebar.classList.toggle("show")
}

// Search functionality
function filterTable(searchTerm, tableId) {
  const table = document.querySelector(`#${tableId} tbody`)
  const rows = table.querySelectorAll("tr")

  rows.forEach((row) => {
    const text = row.textContent.toLowerCase()
    if (text.includes(searchTerm.toLowerCase())) {
      row.style.display = ""
    } else {
      row.style.display = "none"
    }
  })
}

// Event Listeners
document.addEventListener("DOMContentLoaded", () => {
  // Add loading animation to buttons
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

  // Add hover effects to table rows
  const tableRows = document.querySelectorAll("tbody tr")
  tableRows.forEach((row) => {
    row.addEventListener("mouseenter", function () {
      this.style.transform = "scale(1.02)"
    })

    row.addEventListener("mouseleave", function () {
      this.style.transform = "scale(1)"
    })
  })

  // Animate statistics on page load
  const statNumbers = document.querySelectorAll(".stat-number")
  statNumbers.forEach((stat) => {
    const finalValue = stat.textContent
    let currentValue = 0
    const increment = finalValue / 50

    const timer = setInterval(() => {
      currentValue += increment
      if (currentValue >= finalValue) {
        stat.textContent = finalValue
        clearInterval(timer)
      } else {
        stat.textContent = Math.floor(currentValue)
      }
    }, 30)
  })

  // Add search event listeners
  const searchInputs = document.querySelectorAll('input[placeholder*="Search"]')
  searchInputs.forEach((input) => {
    input.addEventListener("input", function () {
      const tableId = this.closest(".content-section").id
      filterTable(this.value, tableId)
    })
  })
})

// Close sidebar when clicking outside on mobile
document.addEventListener("click", (event) => {
  const sidebar = document.getElementById("sidebar")
  const toggle = document.querySelector(".mobile-toggle")

  if (window.innerWidth <= 768 && !sidebar.contains(event.target) && !toggle.contains(event.target)) {
    sidebar.classList.remove("show")
  }
})

// Handle window resize
window.addEventListener("resize", () => {
  const sidebar = document.getElementById("sidebar")
  if (window.innerWidth > 768) {
    sidebar.classList.remove("show")
  }
})
