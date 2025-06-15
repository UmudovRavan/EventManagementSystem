// Navigation functionality (No actual section switching, just for active state if needed)
function showSection(sectionId) {
    // Bu funksiya əslində MVC-də səhifə keçidi üçün istifadə olunmayacaq.
    // Lakin, əgər partial view-lar arasında JavaScript ilə dinamik keçid etmək istəsəniz,
    // bu hissəni istifadə edə bilərsiniz. Hələlik sadəcə active klassı idarə edəcək.
    const sections = document.querySelectorAll('.content-section');
    sections.forEach(section => {
        section.classList.remove('active');
    });

    const targetSection = document.getElementById(sectionId);
    if (targetSection) {
        targetSection.classList.add('active');
    }

    // Update active menu item
    const menuItems = document.querySelectorAll('.sidebar-menu a');
    menuItems.forEach(item => {
        item.classList.remove('active');
    });

    // Find and activate the clicked menu item
    // Bu hissə sidebar elementlərinin `active` klassını idarə etmək üçündür.
    // Əgər event.target yoxdursa, müvafiq menyu elementini tapıb aktiv edir.
    if (event && event.target) {
        event.target.classList.add('active');
    } else {
        const correspondingMenuItem = document.querySelector(`.sidebar-menu a[href*="${sectionId}"]`);
        if (correspondingMenuItem) {
            correspondingMenuItem.classList.add('active');
        }
    }

    // Close mobile sidebar
    if (window.innerWidth <= 768) {
        document.getElementById('sidebar').classList.remove('show');
    }
}

function showDashboard() {
    showSection('dashboard'); // Dashboard aktivləşdirilir.

    // Reset all menu items and activate dashboard
    const menuItems = document.querySelectorAll('.sidebar-menu a');
    menuItems.forEach(item => {
        item.classList.remove('active');
    });
    // Doğru dashboard menyu elementini tapıb aktiv edir.
    // data-section attributu ilə deyil, asp-action ilə işləyən linklər üçün düzəliş edilməlidir.
    // Bu hissəni səhifə yüklənəndə Layout-dan idarə etmək daha yaxşı olar.
    const dashboardMenuItem = document.querySelector('.sidebar-menu a[asp-action="Index"][asp-controller="Home"][asp-area="Admin"]');
    if (dashboardMenuItem) {
        dashboardMenuItem.classList.add('active');
    }
}

function toggleSidebar() {
    const sidebar = document.getElementById('sidebar');
    sidebar.classList.toggle('show');
}

// Close sidebar when clicking outside on mobile
document.addEventListener('click', function (event) {
    const sidebar = document.getElementById('sidebar');
    const toggle = document.querySelector('.mobile-toggle');

    if (window.innerWidth <= 768 &&
        !sidebar.contains(event.target) &&
        !toggle.contains(event.target)) {
        sidebar.classList.remove('show');
    }
});

// Handle window resize
window.addEventListener('resize', function () {
    const sidebar = document.getElementById('sidebar');
    if (window.innerWidth > 768) {
        sidebar.classList.remove('show');
    }
});

function filterTable(searchTerm, tableId) {
    const table = document.querySelector(`#${tableId} table tbody`); // Ensure selecting tbody of specific table
    if (!table) return; // If table not found, exit

    const rows = table.querySelectorAll('tr');

    rows.forEach(row => {
        const text = row.textContent.toLowerCase();
        if (text.includes(searchTerm.toLowerCase())) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
}

// Add search event listeners
document.addEventListener('DOMContentLoaded', function () {
    const searchInputs = document.querySelectorAll('input[placeholder*="Search"]');
    searchInputs.forEach(input => {
        input.addEventListener('input', function () {
            // Find the closest parent section with an ID to determine the table
            const contentSection = this.closest('.content-section');
            if (contentSection) {
                const tableId = contentSection.id; // Use section ID to identify the table context
                filterTable(this.value, tableId);
            }
        });
    });
});