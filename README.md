# MiddleMan

MiddleMan is a comprehensive platform that bridges Europe and China, providing tailored services to support businesses in connecting with suppliers, developing talent, organizing factory tours, and expanding their operations into the Chinese market. With a robust architecture and modern technologies, MiddleMan ensures a seamless experience for users.

## Features

- **Find Suppliers**: Connect with verified and reliable suppliers.
- **Talent Development**: Explore opportunities for personal and professional growth in China.
- **Factory Tours**: Organize and manage tours to inspect and evaluate factories.
- **Expand Your Business**: Receive expert guidance on entering and thriving in the Chinese market.

## Technologies Used

- **Backend**: 
  - C# with Razor Pages for efficient server-side rendering.
  - Controllers manage all application logic.
- **Frontend**: 
  - Vanilla JavaScript and jQuery for interactive and responsive user interfaces.
- **Database**: 
  - Cloud-hosted database using QuickBase for data management.
- **Hosting**: 
  - Deployed on Azure Cloud for reliability and scalability.

## Project Structure

The project is split into **four main projects**, designed for modularity and maintainability:

1. **Main MVC Project**:
   - Serves as the entry point for the application.
   - Contains Razor Pages, controllers, and main application logic.

2. **Class Library: Common**:
   - Includes shared resources like:
     - Text constants used throughout the website.
     - Enums for service and view models.
     - Custom data annotations.
     - Utility classes for simplifying tasks like API requests and XML processing.

3. **Class Library: ViewModels**:
   - Contains models representing objects fetched from the database.
   - Includes models for displaying data on webpages, such as `SitemapModels`.

4. **Class Library: Services**:
   - Houses services utilized by the application.
   - Currently contains:
     - **QuickBase API Folder**:
       - **Service Models**: Abstract representations for interacting with QuickBase API.
       - **Request/Response Models**: Used to structure JSON payloads.
       - **Interface Folder**: Defines the contracts for interacting with external APIs.

## Installation

To run the project:

1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-username/middleman.git
   cd middleman
2. Ensure prerequisites are installed:

   - .NET Core 6.0 or later.

3. Run the application: The application is pre-configured to work out of the box. Simply execute:
     ```bash
    dotnet run --project <path-to-mvc-project>

4. Access the application: Open your browser and navigate to https://localhost:5001 for local testing.

   `Note:` The application is live at https://sinoxpert.eu.

#Usage

   1. Navigate the website to explore services such as supplier connections, talent development, factory tours, and business expansion support.
   2. Benefit from the modular architecture for rapid data handling and seamless API communication.
   3. Leverage the real-time data management powered by QuickBase.(This can be shown on a call as the database access is restricted)

Contributing

We welcome contributions to MiddleMan! Here’s how you can contribute:

  1. Fork this repository.
  2.  Create a branch for your feature or bugfix:
     ```bash

    git checkout -b feature-name

  3. Commit your changes and push them to your branch.
  4. Open a pull request detailing your updates.

For questions, feedback, or support:

    Email: vladimir96930@gmail.com
    Website: SinoXpert.eu
