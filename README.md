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
  "address": "125 Main St, New York, NY",
  "name": "The Grand New York"
}
```

**Response:**
```json
{
  "address": "125 Main St, New York, NY",
  "id": 11,
  "name": "The Grand New York",
  "rooms": []
}
```

##### 400 Bad Request
**Response:**
```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
      "$": [
          "JSON deserialization for type 'HotelManagementSystem.Interfaces.Dto.Hotel' was missing required properties, including the following: name"
      ],
      "hotel": [
          "The hotel field is required."
      ]
  },
  "traceId": "00-77775aba02c1dbcfd23c4e7a2bba878f-c70aa6f6cae4aa04-00"
}
```

##### 422 Client Error
**Response:**
```json
{
  "status": 422,
  "detail": "Hotel with address 125 Main St, New York, NY and name The Grand New York already exists"
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
      "address": "457 Ocean Ave, Miami, FL",
      "id": 2,
      "name": "The Miami Seaside",
      "rooms": []
  },
  {
      "address": "124 Main St, New York, NY",
      "id": 3,
      "name": "The Grand",
      "rooms": []
  },
  {
      "address": "124 Main St, New York, NY",
      "id": 1,
      "name": "The Grand New York",
      "rooms": []
  },
  {
      "address": "Test st. 156, City",
      "id": 10,
      "name": "hotel test 1",
      "rooms": []
  },
  {
      "address": "125 Main St, New York, NY",
      "id": 11,
      "name": "The Grand New York",
      "rooms": []
  }
]
```

---

### Get a Specific Hotel
**Endpoint:** `GET /Api/Hotels/{hotelId}`

#### Response Codes
##### 200 Success
**Response:**
```json
{
  "address": "124 Main St, New York, NY",
  "id": 1,
  "name": "The Grand New York",
  "rooms": []
}
```

##### 404 Not Found
**Response:**
```json
{
  "status": 404,
  "detail": "Hotel 150 could not be found."
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
  "address": "125 Main St, New York, N",
  "id": 11,
  "name": "The Grand New York"
}
```

**Response:**
```json
{
  "address": "125 Main St, New York, N",
  "id": 11,
  "name": "The Grand New York",
  "rooms": []
}
```

##### 400 Bad Request
**Response:**
```json
{
    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "errors": {
        "$": [
            "JSON deserialization for type 'HotelManagementSystem.Interfaces.Dto.Hotel' was missing required properties, including the following: address"
        ],
        "hotel": [
            "The hotel field is required."
        ]
    },
    "traceId": "00-5855db42c38ea9a5b2f343c0efc826b1-a3534a9545dbe2c1-00"
}
```

##### 422 Client Error
**Response:**
```json
{
  "status": 422,
  "detail": "Hotel with address 125 Main St, New York, N and name The Grand New York already exists."
}
```

##### 404 Not Found
**Response:**
```json
{
  "status": 404,
  "detail": "Hotel 50 could not be found"
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
  "status": 404,
  "detail": "Hotel 50 could not be found"
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

