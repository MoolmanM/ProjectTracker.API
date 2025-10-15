## Core Authentication Features
- [x] Basic JWT authentication (register/login)
- [x] User registeration with email
- [x] Password hashing with Identity
- [x] Token authorization

## Refresh Tokens
- [] Store refresh token in database table
- [] Add /api/auth/refresh endpoint
- [] Revoke refresh tokens on logout

## Email
- [] Require email confirmation for new accounts
- [] Password reset via email

## Security
- [] Configure account lockout after failed attempts
- [] Rate limiting on auth endpoints
- [] Forgot password endpoint
- [] Configure lockout duration
- [] Prevent brute force attacks
- [] Add 2FA
- [] Configure MFA settings

## User Profile Management
- [] User profile management endpoints
- [] Account deletion/deactivation
- [] Social Login (Google, Github OAuth)
- [] Change password