# Company Management System (Dockerized)

This project is an **Angular web application** for managing companies. It provides a **frontend interface** that interacts with a **.NET Web API backend**.

---

## ** Features**
###  **Angular Frontend**
 **Displays a list of companies**  
 **Allows adding, updating, and deleting companies**  
 **Interacts with the backend API (`http://backend:5168/api`)**  
 **Environment-based API URL configuration (`environment.ts`)**  
 **Styled with Bootstrap for a clean UI** 

 /company-management/
│── src/                           # Main Angular source code
   ├── app/                       # Contains Angular components, services, and models
│   │   ├── components/            # UI Components (Company List, Company Form)
│   │   │   ├── company-list/      # Displays list of companies
│   │   │   ├── company-form/      # Form to add/edit companies
│   │   ├── services/              # API Calls and data handling
│   │   │   ├── company.service.ts # Handles CRUD operations with backend API
│   │   ├── models/                # Defines data models
│   │   │   ├── company.model.ts   # Defines the Company structure
│   │   ├── app.module.ts          # Main Angular module
│   │   ├── app.component.ts       # Root Angular component
│   │   ├── app-routing.module.ts  # Application routing configuration
│   ├── environments/              # Environment configurations
│   │   ├── environment.ts         # Development API URL
│   │   ├── environment.prod.ts    # Production API URL
│   ├── index.html                 # Main HTML file (entry point)
│   ├── styles.css                 # Global styles
│
│── Dockerfile                     # Defines how Angular is built and served with Nginx
│── nginx.conf                      # Custom Nginx configuration for Angular routing
│── docker-compose.yml              # Docker Compose setup for running the frontend
│── package.json                    # Angular project dependencies
│── angular.json                    # Angular CLI configuration
│── tsconfig.json                    # TypeScript configuration
