# AddisEats.Api

ASP.NET Core Web API for **Addis Eats**, covering health checks, menu items, orders, and restaurants[cite: 15, 16].

---

## Routes & Status Codes

| Method | Full Path | Purpose | Success Code | Failure Codes (Possible / Current) |
| :--- | :--- | :--- | :--- | :--- |
| `GET` | `/api/v1/health` | Return health status, UTC time, and environment name[cite: 15] | `200 OK`[cite: 15] | `500 Internal Server Error` (unhandled exception) |
| `GET` | `/api/v1/menu` | Retrieve all menu items | `200 OK` | `500 Internal Server Error` |
| `GET` | `/api/v1/menu/{id}` | Retrieve a single menu item by `id` | `200 OK` | `400 Bad Request` (non-int `id` via `[ApiController]` model binding), `404 Not Found` (if service/handler returns null/not found), `500 Internal Server Error` |
| `GET` | `/order` | Retrieve orders filtered by `CustomerId` query param | `200 OK` | `400 Bad Request` (invalid/missing `CustomerId` type), `404 Not Found` (when `ordersFound.Count == 0`), `500 Internal Server Error` (if `ordersFound` is null) |
| `POST` | `/order` | Create/submit a new order from request body | `200 OK` *(currently returns `Ok(result)` rather than `201 Created`)* | `400 Bad Request` (invalid JSON or `[ApiController]` model validation failure), `500 Internal Server Error` |
| `GET` | `/api/v1/restaurant` | Search/list restaurants with optional `SearchArea`, `IsActive`, `page`, `pageSize` | `200 OK` | `400 Bad Request` (invalid query param types), `500 Internal Server Error` |
| `GET` | `/api/v1/restaurant/{id:int}` | Retrieve a single restaurant DTO by integer `id` | `200 OK` | `404 Not Found` (non-int `id` fails `:int` constraint, or `rest == null`), `500 Internal Server Error` |
| `GET` | `/api/v1/restaurant/{id}/menu` | Retrieve menu items for a given restaurant `id` | `200 OK` | `400 Bad Request` (non-int `id`), `404 Not Found` (when `result == null`), `500 Internal Server Error` |

---

## Path Segments & Conventions

* **`/api/v1/health`**: `api` (prefix), `v1` (version), `health` (resource) — no identifier or filter segments.
* **`/api/v1/menu`**: `api` (prefix), `v1` (version), `menu` (resource collection) — no identifier or filter segments.
* **`/api/v1/menu/{id}`**: `{id}` is an **identifier** (resource key).
* **`/order`**: `order` is a resource collection — no identifier or filter segments.
* **`/api/v1/restaurant`**: `api` (prefix), `v1` (version), `restaurant` (resource collection) — no identifier or filter segments.
* **`/api/v1/restaurant/{id:int}`**: `{id:int}` is an **identifier** (resource key).
* **`/api/v1/restaurant/{id}/menu`**: `{id}` is an **identifier** (parent restaurant key) and `menu` is a sub-resource collection.

### Planned Route Refinements
* Add `:int` to `/api/v1/restaurant/{id}/menu` (`{id:int}/menu`) to match `/api/v1/restaurant/{id:int}`.
* Align `/order` under `/api/v1/orders` for consistent versioning and plural resource naming.

---

## Error Catalogue

| Status Code | Error Name | Example Message |
| :--- | :--- | :--- |
| `400 Bad Request` | Invalid / Malformed Input | `"One or more validation errors occurred."` |
| `401 Unauthorized` | Missing or Invalid Credentials | `"Authentication token is missing or invalid."` |
| `403 Forbidden` | Insufficient Permissions | `"You do not have permission to perform this action."` |
| `404 Not Found` | Resource Not Found | `"The requested resource was not found."` |
| `409 Conflict` | State / Concurrency Conflict | `"Operation conflicts with the current state of the resource."` |
| `415 Unsupported Media Type` | Unsupported Content-Type | `"Request content type is not supported."` |
| `422 Unprocessable Content` | Semantic Domain Rule Failure | `"Request payload failed business rule validation."` |
| `500 Internal Server Error` | Unhandled Server Error | `"An unexpected error occurred while processing the request."` |

---

## Unknown Query Parameters Policy

Went with the default C# behavior to ignore unkown parameters