# Pocus

***Pocus** project is an improvement to the 'PowerOfControl', using a Crean architectural style.*

***PS**: project name is a combination of PowerOfControl and Focus)* 

## Technologies Used

- **Architeture style**: Clean architecture
- **Backend**: ASP.NET core MVC.
- **Frontend**: Razor pages + HTML, CSS, and JavaScript for building the user interface and client-side functionality.
- **Data Management**: MSSQL.
- **Authentication**: Custom secure authentication mechanisms implemented for user login and account management.
- **Concentration Timer**: Custom JavaScript functionality integrated for implementing the concentration timer feature.
- **Libraries**: Bogus, FluentValidation, SignalR, MailKit.

## Features

### Authentication and User Management
- User-friendly authentication system allowing users to create accounts and log in securely.
- Options for users to modify their account information, change passwords, and manage email preferences.
- Robust validation mechanisms to ensure the accuracy of user input and prompt error messages for incorrect information.

### Dashboard
- A central hub for easy navigation to all sections of the application, providing a seamless user experience.

### Task Management
- Comprehensive task management functionality including creating, deleting, archiving, and copying tasks.

### Habit Tracking
- Facility for users to create lists of desired habits to track their progress.

### Concentration Timer
- Customizable concentration mode with the ability to set duration and toggle breaks.
- Display of goal completion status and minimal statistics to track progress.
- Selection of tasks, notes, and habits for each work sprint.
- Activation of concentration mode to enhance focus during work sessions.

### Settings
- Configurable parameters for concentration periods, goal settings, and application preferences.
- Options to adjust work time, break time, notifications, sound effects, and more.
- Personalization features such as theme colors, statistics display preferences, and website blocking settings.

### Performance Statistics (in progress)
- Interactive tables and charts for tracking habit completion and work session durations.
- Visual representation of productivity metrics through graphs showcasing completed tasks and work time.

## Installation

To install **Pocus**, follow these steps:

1. Clone the repository to your local machine.
2. Set up the backend using ASP.NET core MVC, ensuring MSSQL is correctly configured as the database.
3. Configure the frontend with HTML, CSS, and JavaScript.
4. Run the application locally or deploy it to a hosting environment.

## Usage

Once installed, users can access **Power of Control** through a web browser. They can then:

- Sign up for an account or log in if they already have one.
- Navigate through the dashboard to access various features.
- Manage tasks, habits, and concentration sessions according to their daily goals.
- Customize settings to tailor the application to their preferences.
- Track performance and productivity using the built-in statistics tools.
