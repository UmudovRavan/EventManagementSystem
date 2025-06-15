// Create floating particles
function createParticles() {
    const particlesContainer = document.getElementById('particles');
    const particleCount = 15;

    for (let i = 0; i < particleCount; i++) {
        const particle = document.createElement('div');
        particle.className = 'particle';
        particle.style.left = Math.random() * 100 + '%';
        particle.style.top = Math.random() * 100 + '%';
        particle.style.animationDelay = Math.random() * 6 + 's';
        particle.style.animationDuration = (Math.random() * 3 + 3) + 's';
        particlesContainer.appendChild(particle);
    }
}

// Toggle password visibility
document.getElementById('togglePassword').addEventListener('click', function() {
    const passwordInput = document.getElementById('password');
    const icon = this;
    
    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        icon.classList.remove('fa-eye');
        icon.classList.add('fa-eye-slash');
    } else {
        passwordInput.type = 'password';
        icon.classList.remove('fa-eye-slash');
        icon.classList.add('fa-eye');
    }
});

// Form validation
function validateEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}

function validatePassword(password) {
    return password.length >= 6;
}

function showError(fieldId, message) {
    const errorElement = document.getElementById(fieldId + 'Error');
    const inputElement = document.getElementById(fieldId);
    
    errorElement.textContent = message;
    errorElement.style.display = 'block';
    inputElement.style.borderColor = 'var(--danger)';
    
    setTimeout(() => {
        errorElement.style.display = 'none';
        inputElement.style.borderColor = '#e9ecef';
    }, 3000);
}

function showSuccess(message) {
    // You can implement a success message display here
    console.log('Success:', message);
}

// Form submission
document.getElementById('signInForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    const email = document.getElementById('email').value.trim();
    const password = document.getElementById('password').value;
    const loading = document.getElementById('loading');
    const buttonText = document.getElementById('buttonText');
    
    // Reset previous errors
    document.querySelectorAll('.error-message').forEach(error => {
        error.style.display = 'none';
    });
    
    document.querySelectorAll('.form-control').forEach(input => {
        input.style.borderColor = '#e9ecef';
    });

    let isValid = true;

    // Validate email
    if (!email) {
        showError('email', 'Email is required');
        isValid = false;
    } else if (!validateEmail(email)) {
        showError('email', 'Please enter a valid email address');
        isValid = false;
    }

    // Validate password
    if (!password) {
        showError('password', 'Password is required');
        isValid = false;
    } else if (!validatePassword(password)) {
        showError('password', 'Password must be at least 6 characters');
        isValid = false;
    }

    if (isValid) {
        // Show loading state
        loading.style.display = 'inline-block';
        buttonText.textContent = 'Signing In...';
        
        // Simulate API call
        setTimeout(() => {
            loading.style.display = 'none';
            buttonText.textContent = 'Sign In';
            
            // Simulate successful login
            showSuccess('Login successful!');
            
            // Redirect to main page
            // MVC-də bu hissəni server tərəfindən yönləndirməklə əvəz etməlisiniz.
            // Məsələn, AJAX sorğusundan sonra Controller-dən yönləndirmə.
            // Və ya, əgər bu birbaşa POST formudursa, bu setTimeout-u silin.
            setTimeout(() => {
                window.location.href = 'index.html'; // Bu sətir MVC-də dəyişdirilməlidir
            }, 1000);
        }, 2000);
    }
});

// Forgot password functionality
document.getElementById('forgotPasswordLink').addEventListener('click', function(e) {
    e.preventDefault();
    
    const email = prompt('Please enter your email address:');
    if (email && validateEmail(email)) {
        alert('Password reset link has been sent to your email!');
    } else if (email) {
        alert('Please enter a valid email address.');
    }
});

// Social login handlers
document.querySelectorAll('.social-btn').forEach(btn => {
    btn.addEventListener('click', function(e) {
        e.preventDefault();
        const provider = this.textContent.trim();
        alert(`${provider} login will be implemented soon!`);
    });
});

// Input focus animations
document.querySelectorAll('.form-control').forEach(input => {
    input.addEventListener('focus', function() {
        this.parentElement.classList.add('focused');
    });
    
    input.addEventListener('blur', function() {
        this.parentElement.classList.remove('focused');
    });
});

// Initialize particles on page load and add entrance animation to form elements
document.addEventListener('DOMContentLoaded', function() {
    createParticles();
    
    const formElements = document.querySelectorAll('.form-group, .social-login, .btn-primary');
    formElements.forEach((element, index) => {
        element.style.opacity = '0';
        element.style.transform = 'translateY(20px)';
        
        setTimeout(() => {
            element.style.transition = 'all 0.6s ease';
            element.style.opacity = '1';
            element.style.transform = 'translateY(0)';
        }, index * 100 + 300);
    });
});