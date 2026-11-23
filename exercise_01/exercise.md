# Addis Eats - API Trace & Endpoint Specification

This document details the Network tab analysis of the **Addis Eats** web application, analyzing active HTTP requests, header key-value pairs, status code behaviors, and the proposed API schema design for future development.

## 1. Traced API Requests

The following table records six distinct requests captured using the browser DevTools Network tab during navigation across the Addis Eats application.

| \# | HTTP Method | Full Path / Request URL | Status Code | Request Body Included? | 
| ----- | ----- | ----- | ----- | ----- | 
| **1** | `GET` | `http://localhost:3000/` | `200 OK` | No | 
| **2** | `GET` | `http://localhost:3000/menu` | `200 OK` | No | 
| **3** | `GET` | `https://addis-eats-backend.onrender.com/menu/` | `304 Not Modified` | No | 
| **4** | `GET` | `http://localhost:3000/menu/menu-10?_rsc=m_PhElRAUkzUL3J6` | `200 OK` | No | 
| **5** | `GET` | `http://localhost:3000/cart?_rsc=m_PhElRAUkzUL3J6` | `307 Temporary Redirect` | No | 
| **6** | `GET` | `https://addis-eats-backend.onrender.com/menu/` | `304 Not Modified` | No | 

## 2. Header Analysis

Detailed header breakdown for two selected requests from the Network trace session.\
\
The requests analyzed here doesn’t require authentication form the user side thus the headers are mostly empty for the request.

### Request A: React Server Component (RSC) Segment Data

* **URL:** `http://localhost:3000/menu/menu-10?_rsc=m_PhElRAUkzUL3J6`

* **HTTP Method:** `GET` | **Status Code:** `200 OK`

#### Response Headers:

1. `Cache-Control: no-store`

   * **Purpose:** Directs client browsers and intermediate proxies not to store or cache the payload, ensuring fresh client-side payload evaluation.

2. `Content-Encoding: gzip`

   * **Purpose:** Specifies that the server compressed the HTTP response payload using gzip to reduce bandwidth usage.

3. `Content-Type: text/x-component`

   * **Purpose:** Identifies the payload as a Next.js React Server Component (RSC) flight data stream rather than standard HTML or raw JSON.

### Request B: Remote API Backend Fetch

* **URL:** `https://addis-eats-backend.onrender.com/menu/`

* **HTTP Method:** `GET` | **Status Code:** `304 Not Modified`

#### Response Headers:

1. `Access-Control-Allow-Origin: *`

   * **Purpose:** Enables Cross-Origin Resource Sharing (CORS) by permitting requests originating from any domain (e.g., `http://localhost:3000`).

2. `Alt-Svc: h3=":443"; ma=86400`

   * **Purpose:** Informs the browser that HTTP/3 service is available on port 443 for up to 86,400 seconds (24 hours).

3. `Cf-Cache-Status: DYNAMIC`

   * **Purpose:** Indicates that Cloudflare handled the request dynamically without serving a static edge-cached copy.

## 3. Status Code Evaluation & Analysis

### Observed Status Code: `307 Temporary Redirect`

* **Target Path:** `http://localhost:3000/cart?_rsc=m_PhElRAUkzUL3J6`

* **Analysis & Justification:**

  * **Was `307` the correct status code?**

    Yes. A `307 Temporary Redirect` is appropriate when a route guard or middleware redirects the client (for example, when redirecting an unauthenticated user or an empty cart state) while explicitly guaranteeing that the client preserves the original HTTP method (`GET`) and query parameters during the redirection step.

### Additional Error Code Design Considerations

* **Authentication Failure (`401 Unauthorized`):** Should be returned if an unauthenticated user attempts to place an order (`POST /api/orders`).

* **Resource Conflict (`409 Conflict`):** Should be returned if an order state transition is invalid (e.g., attempting `POST /api/orders/123/cancel` on an order already marked as `DELIVERED`).

## 4. Addis Eats API Endpoint Specification

Proposed RESTful API design for the backend service powering Addis Eats.

| Method | Path | Purpose / Description | Success Code | 
| ----- | ----- | ----- | ----- | 
| `GET` | `/api/menu` | Fetch all menu items with category filtering support | `200 OK` | 
| `GET` | `/api/menu/:id` | Retrieve detailed information for a single dish | `200 OK` | 
| `POST` | `/api/cart/items` | Add a dish and quantity to the current user's cart | `201 Created` | 
| `PATCH` | `/api/cart/items/:itemId` | Update quantity or special instructions for a cart item | `200 OK` | 
| `POST` | `/api/orders` | Checkout and submit the current cart contents as an order | `201 Created` | 
| `GET` | `/api/orders` | Retrieve order history for the authenticated user | `200 OK` | 
