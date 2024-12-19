# Hotel Management System API

This document provides an overview of the Hotel Management System API, including endpoints, request methods, response codes, and examples.

---

## Table of Contents
- [Hotels API](#hotels-api)
- [Reservations API](#reservations-api)
- [Rooms API](#rooms-api)
- [Users API](#users-api)

---

## Hotels API

### Create a Hotel
**Endpoint:** `POST /Api/Hotels`

#### Response Codes
##### 201 Created
**Request:**
```json
{
  "name": "Sunrise Inn",
  "address": "123 Ocean Drive",
  "rooms": [
    {
      "roomNumber": "101",
      "capacity": 2
    }
  ]
}
```

**Response:**
```json
{
  "id": 1,
  "name": "Sunrise Inn",
  "address": "123 Ocean Drive",
  "rooms": [
    {
      "id": 10,
      "roomNumber": "101",
      "capacity": 2
    }
  ]
}
```

##### 400 Bad Request
**Response:**
```json
{
  "type": "https://example.com/probs/invalid-data",
  "title": "Invalid Data",
  "status": 400,
  "detail": "Hotel name is required."
}
```

##### 422 Client Error
**Response:**
```json
{
  "type": "https://example.com/probs/validation-error",
  "title": "Validation Error",
  "status": 422,
  "detail": "Room capacity must be a positive integer."
}
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

### Get All Hotels
**Endpoint:** `GET /Api/Hotels`

#### Response Codes
##### 200 Success
**Response:**
```json
[
  {
    "id": 1,
    "name": "Sunrise Inn",
    "address": "123 Ocean Drive"
  },
  {
    "id": 2,
    "name": "Mountain Retreat",
    "address": "456 Alpine Lane"
  }
]
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

### Get a Specific Hotel
**Endpoint:** `GET /Api/Hotels/{hotelId}`

#### Response Codes
##### 200 Success
**Response:**
```json
{
  "id": 1,
  "name": "Sunrise Inn",
  "address": "123 Ocean Drive",
  "rooms": [
    {
      "id": 10,
      "roomNumber": "101",
      "capacity": 2
    }
  ]
}
```

##### 404 Not Found
**Response:**
```json
{
  "type": "https://example.com/probs/not-found",
  "title": "Hotel Not Found",
  "status": 404,
  "detail": "Hotel with ID 100 not found."
}
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

### Update a Hotel
**Endpoint:** `PUT /Api/Hotels/Update`

#### Response Codes
##### 200 Success
**Request:**
```json
{
  "id": 1,
  "name": "Sunrise Inn - Updated",
  "address": "456 Ocean Drive"
}
```

**Response:**
```json
{
  "id": 1,
  "name": "Sunrise Inn - Updated",
  "address": "456 Ocean Drive",
  "rooms": []
}
```

##### 400 Bad Request
**Response:**
```json
{
  "type": "https://example.com/probs/invalid-data",
  "title": "Invalid Data",
  "status": 400,
  "detail": "Hotel ID must be provided."
}
```

##### 422 Client Error
**Response:**
```json
{
  "type": "https://example.com/probs/validation-error",
  "title": "Validation Error",
  "status": 422,
  "detail": "Hotel name must be between 1 and 100 characters."
}
```

##### 404 Not Found
**Response:**
```json
{
  "type": "https://example.com/probs/not-found",
  "title": "Hotel Not Found",
  "status": 404,
  "detail": "Hotel with ID 100 not found."
}
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

### Delete a Hotel
**Endpoint:** `DELETE /Api/Hotels/{hotelId}`

#### Response Codes
##### 204 No Content
**Response:** _(No Content)_

##### 404 Not Found
**Response:**
```json
{
  "type": "https://example.com/probs/not-found",
  "title": "Hotel Not Found",
  "status": 404,
  "detail": "Hotel with ID 100 not found."
}
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

## Reservations API

### Create a Reservation
**Endpoint:** `POST /Api/Reservations`

#### Response Codes
##### 201 Created
**Request:**
```json
{
  "roomId": 101,
  "userId": "abc123",
  "checkInDate": "2024-12-25T14:00:00",
  "checkOutDate": "2024-12-30T10:00:00",
  "guestCount": 2
}
```

**Response:**
```json
{
  "id": 1,
  "roomId": 101,
  "userId": "abc123",
  "checkInDate": "2024-12-25T14:00:00",
  "checkOutDate": "2024-12-30T10:00:00",
  "guestCount": 2
}
```

##### 400 Bad Request
**Response:**
```json
{
  "type": "https://example.com/probs/invalid-data",
  "title": "Invalid Data",
  "status": 400,
  "detail": "Check-in date must be before check-out date."
}
```

##### 422 Client Error
**Response:**
```json
{
  "type": "https://example.com/probs/validation-error",
  "title": "Validation Error",
  "status": 422,
  "detail": "Room ID must be valid."
}
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

### Get All Reservations
**Endpoint:** `GET /Api/Reservations`

#### Response Codes
##### 200 Success
**Response:**
```json
[
  {
    "id": 1,
    "roomId": 101,
    "userId": "abc123",
    "checkInDate": "2024-12-25T14:00:00",
    "checkOutDate": "2024-12-30T10:00:00",
    "guestCount": 2
  }
]
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

### Get a Specific Reservation
**Endpoint:** `GET /Api/Reservations/{reservationId}`

#### Response Codes
##### 200 Success
**Response:**
```json
{
  "id": 1,
  "roomId": 101,
  "userId": "abc123",
  "checkInDate": "2024-12-25T14:00:00",
  "checkOutDate": "2024-12-30T10:00:00",
  "guestCount": 2
}
```

##### 404 Not Found
**Response:**
```json
{
  "type": "https://example.com/probs/not-found",
  "title": "Reservation Not Found",
  "status": 404,
  "detail": "Reservation with ID 200 not found."
}
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

### Update a Reservation
**Endpoint:** `PUT /Api/Reservations/Update`

#### Response Codes
##### 200 Success
**Request:**
```json
{
  "id": 1,
  "roomId": 101,
  "userId": "abc123",
  "checkInDate": "2024-12-26T14:00:00",
  "checkOutDate": "2024-12-31T10:00:00",
  "guestCount": 2
}
```

**Response:**
```json
{
  "id": 1,
  "roomId": 101,
  "userId": "abc123",
  "checkInDate": "2024-12-26T14:00:00",
  "checkOutDate": "2024-12-31T10:00:00",
  "guestCount": 2
}
```

##### 400 Bad Request
**Response:**
```json
{
  "type": "https://example.com/probs/invalid-data",
  "title": "Invalid Data",
  "status": 400,
  "detail": "Check-in date must be before check-out date."
}
```

##### 422 Client Error
**Response:**
```json
{
  "type": "https://example.com/probs/validation-error",
  "title": "Validation Error",
  "status": 422,
  "detail": "Room ID must be valid."
}
```

##### 404 Not Found
**Response:**
```json
{
  "type": "https://example.com/probs/not-found",
  "title": "Reservation Not Found",
  "status": 404,
  "detail": "Reservation with ID 200 not found."
}
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

### Delete a Reservation
**Endpoint:** `DELETE /Api/Reservations/{reservationId}`

#### Response Codes
##### 204 No Content
**Response:** _(No Content)_

##### 404 Not Found
**Response:**
```json
{
  "type": "https://example.com/probs/not-found",
  "title": "Reservation Not Found",
  "status": 404,
  "detail": "Reservation with ID 200 not found."
}
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

## Rooms API

### Create a Room
**Endpoint:** `POST /Api/Rooms`

#### Response Codes
##### 201 Created
**Request:**
```json
{
  "roomNumber": "202",
  "capacity": 3,
  "hotelId": 1
}
```

**Response:**
```json
{
  "id": 2,
  "roomNumber": "202",
  "capacity": 3,
  "hotelId": 1
}
```

##### 400 Bad Request
**Response:**
```json
{
  "type": "https://example.com/probs/invalid-data",
  "title": "Invalid Data",
  "status": 400,
  "detail": "Room number must be provided."
}
```

##### 422 Client Error
**Response:**
```json
{
  "type": "https://example.com/probs/validation-error",
  "title": "Validation Error",
  "status": 422,
  "detail": "Capacity must be greater than zero."
}
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

### Get All Rooms
**Endpoint:** `GET /Api/Rooms`

#### Response Codes
##### 200 Success
**Response:**
```json
[
  {
    "id": 1,
    "roomNumber": "101",
    "capacity": 2,
    "hotelId": 1
  },
  {
    "id": 2,
    "roomNumber": "202",
    "capacity": 3,
    "hotelId": 1
  }
]
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

### Get a Specific Room
**Endpoint:** `GET /Api/Rooms/{roomId}`

#### Response Codes
##### 200 Success
**Response:**
```json
{
  "id": 1,
  "roomNumber": "101",
  "capacity": 2,
  "hotelId": 1
}
```

##### 404 Not Found
**Response:**
```json
{
  "type": "https://example.com/probs/not-found",
  "title": "Room Not Found",
  "status": 404,
  "detail": "Room with ID 50 not found."
}
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

### Update a Room
**Endpoint:** `PUT /Api/Rooms/Update`

#### Response Codes
##### 200 Success
**Request:**
```json
{
  "id": 1,
  "roomNumber": "101A",
  "capacity": 2,
  "hotelId": 1
}
```

**Response:**
```json
{
  "id": 1,
  "roomNumber": "101A",
  "capacity": 2,
  "hotelId": 1
}
```

##### 400 Bad Request
**Response:**
```json
{
  "type": "https://example.com/probs/invalid-data",
  "title": "Invalid Data",
  "status": 400,
  "detail": "Room ID must be provided."
}
```

# Hotel Management System API (Continued)

...

##### 422 Client Error (Continued)
**Response:**
```json
{
  "type": "https://example.com/probs/validation-error",
  "title": "Validation Error",
  "status": 422,
  "detail": "Capacity must be greater than zero and room number must be unique."
}
```

##### 404 Not Found
**Response:**
```json
{
  "type": "https://example.com/probs/not-found",
  "title": "Room Not Found",
  "status": 404,
  "detail": "Room with ID 100 not found."
}
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

### Delete a Room
**Endpoint:** `DELETE /Api/Rooms/{roomId}`

#### Response Codes
##### 204 No Content
**Response:** _(No Content)_

##### 404 Not Found
**Response:**
```json
{
  "type": "https://example.com/probs/not-found",
  "title": "Room Not Found",
  "status": 404,
  "detail": "Room with ID 100 not found."
}
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

## Users API

### Register a User
**Endpoint:** `POST /Api/Users/Register`

#### Response Codes
##### 201 Created
**Request:**
```json
{
  "userName": "jdoe",
  "password": "SecurePass123!",
  "passwordConfirmation": "SecurePass123!",
  "email": "jdoe@example.com",
  "name": "John",
  "surname": "Doe"
}
```

**Response:**
```json
{
  "id": "123e4567-e89b-12d3-a456-426614174000",
  "userName": "jdoe",
  "email": "jdoe@example.com",
  "name": "John",
  "surname": "Doe"
}
```

##### 400 Bad Request
**Response:**
```json
{
  "type": "https://example.com/probs/invalid-data",
  "title": "Invalid Data",
  "status": 400,
  "detail": "Passwords do not match."
}
```

##### 422 Client Error
**Response:**
```json
{
  "type": "https://example.com/probs/validation-error",
  "title": "Validation Error",
  "status": 422,
  "detail": "Password must contain at least 8 characters, including uppercase, lowercase, a digit, and a special character."
}
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

### Login a User
**Endpoint:** `POST /Api/Users/Login`

#### Response Codes
##### 200 Success
**Request:**
```json
{
  "userName": "jdoe",
  "password": "SecurePass123!"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "dGhpc2lzYXJlZnJlc2h0b2tlbg=="
}
```

##### 400 Bad Request
**Response:**
```json
{
  "type": "https://example.com/probs/invalid-data",
  "title": "Invalid Credentials",
  "status": 400,
  "detail": "The username or password is incorrect."
}
```

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

### Logout a User
**Endpoint:** `POST /Api/Users/Logout`

#### Response Codes
##### 200 Success
**Response:** _(No Content)_

##### 500 Server Error
**Response:**
```json
{
  "type": "https://example.com/probs/internal-server-error",
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An unexpected error occurred."
}
```

---

This concludes the comprehensive documentation for the Hotel Management System API. For additional details or clarifications, please refer to the API source or contact the support team.

