### **Step 1: Build Angular App**
# Used a **Node.js image** to install dependencies and build the app.
#dockerfile

FROM node:18 AS build-stage
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build --configuration=production

# Debug: Show files before copying
RUN ls -la /app/dist/company-management/browser/

# Serve with Nginx
FROM nginx:alpine AS production-stage
WORKDIR /usr/share/nginx/html
RUN rm -rf /usr/share/nginx/html/*
COPY --from=build-stage /app/dist/company-management/browser /usr/share/nginx/html

COPY nginx.conf /etc/nginx/nginx.conf

# Fix permissions
RUN chmod -R 755 /usr/share/nginx/html && \
    chown -R nginx:nginx /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]







