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
function setupPasswordToggle(toggleId, passwordId) {
    document.getElementById(toggleId).addEventListener('click', function() {
        const passwordInput = document.getElementById(passwordId);
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
}

// Password strength checker
function checkPasswordStrength(password) {
    let strength = 0;
    let feedback = [];

    if (password.length >= 8) strength += 1;
    else feedback.push('at least 8 characters');

    if (/[a-z]/.test(password)) strength += 1;
    else feedback.push('lowercase letter');

    if (/[A-Z]/.test(password)) strength += 1;
    else feedback.push('uppercase letter');

    if (/[0-9]/.test(password)) strength += 1;
    else feedback.push('number');

    if (/[^A-Za-z0-9]/.test(password)) strength += 1;
    else feedback.push('special character');

    return { strength, feedback };
}

function updatePasswordStrength(password) {
    const { strength, feedback } = checkPasswordStrength(password);
    const strengthFill = document.getElementById('strengthFill');
    const strengthText = document.getElementById('strengthText');

    const percentage = (strength / 5) * 100;
    strengthFill.style.width = percentage + '%';

    if (strength <= 2) {
        strengthFill.className = 'strength-fill strength-weak';
        strengthText.textContent = 'Weak password';
    } else if (strength <= 3) {
        strengthFill.className = 'strength-fill strength-medium';
        strengthText.textContent = 'Medium password';
    } else {
        strengthFill.className = 'strength-fill strength-strong';
        strengthText.textContent = 'Strong password';
    }

    return strength >= 3;
}

// Form validation functions
function validateEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}

function validateName(name) {
    return name.trim().length >= 2;
}

function showError(fieldId, message) {
    const errorElement = document.getElementById(fieldId + 'Error');
    const inputElement = document.getElementById(fieldId);
    
    errorElement.textContent = message;
    errorElement.style.display = 'block';
    inputElement.classList.add('invalid');
    inputElement.classList.remove('valid');
    
    setTimeout(() => {
        errorElement.style.display = 'none';
        inputElement.classList.remove('invalid');
    }, 5000);
}

function showSuccess(fieldId, message) {
    const successElement = document.getElementById(fieldId + 'Success');
    const inputElement = document.getElementById(fieldId);
    
    if (successElement) {
        successElement.textContent = message;
        successElement.style.display = 'block';
        inputElement.classList.add('valid');
        inputElement.classList.remove('invalid');
        
        setTimeout(() => {
            successElement.style.display = 'none';
        }, 3000);
    }
}

// Real-time email validation
document.getElementById('email').addEventListener('blur', function() {
    const email = this.value.trim();
    if (email && validateEmail(email)) {
        showSuccess('email', 'Email looks good!');
    }
});

// Password strength real-time checking
document.getElementById('password').addEventListener('input', function() {
    updatePasswordStrength(this.value);
});

// Confirm password real-time checking
document.getElementById('confirmPassword').addEventListener('input', function() {
    const password = document.getElementById('password').value;
    const confirmPassword = this.value;
    
    if (confirmPassword && password !== confirmPassword) {
        this.classList.add('invalid');
        this.classList.remove('valid');
    } else if (confirmPassword && password === confirmPassword) {
        this.classList.add('valid');
        this.classList.remove('invalid');
    }
});

// Form submission
document.getElementById('signUpForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    const formData = {
        firstName: document.getElementById('firstName').value.trim(),
        lastName: document.getElementById('lastName').value.trim(),
        email: document.getElementById('email').value.trim(),
        password: document.getElementById('password').value,
        confirmPassword: document.getElementById('confirmPassword').value,
        role: document.getElementById('role').value,
        terms: document.getElementById('terms').checked
    };
    
    const loading = document.getElementById('loading');
    const buttonText = document.getElementById('buttonText');
    
    // Reset previous errors
    document.querySelectorAll('.error-message').forEach(error => {
        error.style.display = 'none';
    });
    
    document.querySelectorAll('.form-control').forEach(input => {
        input.classList.remove('invalid');
    });

    let isValid = true;

    // Validate first name
    if (!formData.firstName) {
        showError('firstName', 'First name is required');
        isValid = false;
    } else if (!validateName(formData.firstName)) {
        showError('firstName', 'First name must be at least 2 characters');
        isValid = false;
    }

    // Validate last name
    if (!formData.lastName) {
        showError('lastName', 'Last name is required');
        isValid = false;
    } else if (!validateName(formData.lastName)) {
        showError('lastName', 'Last name must be at least 2 characters');
        isValid = false;
    }

    // Validate email
    if (!formData.email) {
        showError('email', 'Email is required');
        isValid = false;
    } else if (!validateEmail(formData.email)) {
        showError('email', 'Please enter a valid email address');
        isValid = false;
    }

    // Validate password
    if (!formData.password) {
        showError('password', 'Password is required');
        isValid = false;
    } else if (!updatePasswordStrength(formData.password)) {
        showError('password', 'Password is too weak');
        isValid = false;
    }

    // Validate confirm password
    if (!formData.confirmPassword) {
        showError('confirmPassword', 'Please confirm your password');
        isValid = false;
    } else if (formData.password !== formData.confirmPassword) {
        showError('confirmPassword', 'Passwords do not match');
        isValid = false;
    }

    // Validate role
    if (!formData.role) {
        showError('role', 'Please select your role');
        isValid = false;
    }

    // Validate terms
    if (!formData.terms) {
        showError('terms', 'You must agree to the terms and conditions');
        isValid = false;
    }

    if (isValid) {
        // Show loading state
        loading.style.display = 'inline-block';
        buttonText.textContent = 'Creating Account...';
        
        // Simulate API call
        setTimeout(() => {
            loading.style.display = 'none';
            buttonText.textContent = 'Create Account';
            
            // Simulate successful registration
            alert('Account created successfully! Please check your email to verify your account.');
            
            // Redirect to sign in page
            // Bu hissə MVC-də server tərəfindən idarə olunmalıdır.
            // Yəni, əgər bu birbaşa POST formudursa, bu setTimeout-u silin.
            setTimeout(() => {
                window.location.href = 'signIn.html'; // MVC-də bu sətir dəyişdirilməlidir
            }, 1000);
        }, 2500);
    }
});

// Social login handlers
document.querySelectorAll('.social-btn').forEach(btn => {
    btn.addEventListener('click', function(e) {
        e.preventDefault();
        const provider = this.textContent.trim();
        alert(`${provider} registration will be implemented soon!`);
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

// Initialize everything on page load
document.addEventListener('DOMContentLoaded', function() {
    createParticles();
    setupPasswordToggle('togglePassword', 'password');
    setupPasswordToggle('toggleConfirmPassword', 'confirmPassword');
    
    // Add entrance animation to form elements
    const formElements = document.querySelectorAll('.form-group, .form-row, .social-login, .btn-primary, .terms-checkbox');
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