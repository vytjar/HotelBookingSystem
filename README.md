# **Viešbučio kambarių rezervacijos sistema**

### Pagrindiniai taikomosios srities objektai:
- Viešbutis → Kambarys → Rezervacija

## Funkciniai Reikalavimai

### Rolės:
- Svečias: Gali peržiūrėti viešbučius ir kambarių užimtumą.
- Naudotojas: Gali rezervuoti kambarius, peržiūrėti jau atliktas rezervacijas ir jas atšaukti.
- Administratorius Gali: valdyti viešbučius, viešbučių kambarius, visas rezervacijas.

### Naudojamos technologijos

- **Backend**: .NET Core
- **Frontend**: Vue.JS
- **Duomenų bazė**: PostgreSQL
- **Hostingas**: Azure

## Deployment diagrama

![image](https://github.com/user-attachments/assets/fa511157-1e78-4335-81d1-3fa8d4fee658)

## Turinys
- [Hotels API](#hotels-api)
- [Reservations API](#reservations-api)
- [Rooms API](#rooms-api)
- [Users API](#users-api)

---

## Hotels API

### Create a Hotel
**Authentication**: Bearer token
**Authorization**: Admin
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
**Authentication**: Bearer token
**Authorization**: Admin
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
**Authentication**: Bearer token
**Authorization**: Admin
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
**Authentication**: Bearer token
**Authorization**: User, Admin
**Endpoint:** `POST /Api/Reservations`

#### Response Codes
##### 201 Created
**Request:**
```json
{
  "checkInDate": "2024-12-21",
  "checkOutDate": "2024-12-22",
  "guestCount": 2,
  "roomId": 12
}
```

**Response:**
```json
{
  "checkInDate": "2024-12-21T00:00:00",
  "checkOutDate": "2024-12-22T00:00:00",
  "guestCount": 2,
  "id": 50,
  "room": null,
  "roomId": 12,
  "userId": "9cd39d64-0273-4b34-8705-5abd4f652313",
  "user": null
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
            "JSON deserialization for type 'HotelManagementSystem.Interfaces.Dto.Reservation' was missing required properties, including the following: checkOutDate"
        ],
        "reservation": [
            "The reservation field is required."
        ]
    },
    "traceId": "00-441dc9701676b219b3d6641d87bae433-98aaa073f014089c-00"
}
```

##### 422 Client Error
**Response:**
```json
{
  "status": 422,
  "detail": "Check out date 2024-12-12 can not be before or on the same day as the check in date 2024-12-16"
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
    "checkInDate": "2024-12-16T02:00:00Z",
    "checkOutDate": "2024-12-17T02:00:00Z",
    "guestCount": 1,
    "id": 1,
    "room": null,
    "roomId": 12,
    "userId": "9cd39d64-0273-4b34-8705-5abd4f652313",
    "user": null
  },
  {
    "checkInDate": "2024-10-01T14:00:00Z",
    "checkOutDate": "2024-10-03T11:00:00Z",
    "guestCount": 2,
    "id": 2,
    "room": null,
    "roomId": 3,
    "userId": "7ab5d8f1-1374-4335-abd1-ebccecccdf71",
    "user": null
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
  "checkInDate": "2024-10-01T14:00:00Z",
  "checkOutDate": "2024-10-03T11:00:00Z",
  "guestCount": 2,
  "id": 2,
  "room": null,
  "roomId": 3,
  "userId": "7ab5d8f1-1374-4335-abd1-ebccecccdf71",
  "user": null
}
```

##### 404 Not Found
**Response:**
```json
{
  "status": 404,
  "detail": "Reservation 55 could not be found"
}
```

---

### Update a Reservation
**Authentication**: Bearer token
**Authorization**: User, Admin
**Endpoint:** `PUT /Api/Reservations/Update`

#### Response Codes
##### 200 Success
**Request:**
```json
{
  "checkInDate": "2024-12-21T00:00:00",
  "checkOutDate": "2024-12-22T00:00:00",
  "guestCount": 2,
  "id": 50,
  "roomId": 12
}
```

**Response:**
```json
{
  "checkInDate": "2024-12-21T00:00:00Z",
  "checkOutDate": "2024-12-22T00:00:00Z",
  "guestCount": 2,
  "id": 50,
  "room": null,
  "roomId": 12,
  "userId": "9cd39d64-0273-4b34-8705-5abd4f652313"
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
          "JSON deserialization for type 'HotelManagementSystem.Interfaces.Dto.Reservation' was missing required properties, including the following: roomId"
      ],
      "reservation": [
          "The reservation field is required."
      ]
  },
  "traceId": "00-bbdcc6215b556014490edd9714739412-6e14f2cd4bbd41ed-00"
}
```

##### 422 Client Error
**Response:**
```json
{
  "status": 422,
  "detail": "Guest count -1 can not be lower than 1"
}
```

##### 404 Not Found
**Response:**
```json
{
  "status": 404,
  "detail": "Room 5000 could not be found"
}
```

---

### Delete a Reservation
**Authentication**: Bearer token
**Authorization**: User, Admin
**Endpoint:** `DELETE /Api/Reservations/{reservationId}`

#### Response Codes
##### 204 No Content
**Response:** _(No Content)_

##### 404 Not Found
**Response:**
```json
{
  "status": 404,
  "detail": "Reservation 500 could not be found"
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
  "capacity": 2,
  "hotelId": 1,
  "roomNumber": "T2"
}
```

**Response:**
```json
{
  "capacity": 2,
  "hotel": null,
  "hotelId": 1,
  "id": 24,
  "reservations": [],
  "roomNumber": "T2"
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
          "JSON deserialization for type 'HotelManagementSystem.Interfaces.Dto.Room' was missing required properties, including the following: hotelId"
      ],
      "room": [
          "The room field is required."
      ]
  },
  "traceId": "00-fdecde71a1ac0c64e4ddd5fa619fd234-31eb9294f5b2d246-00"
}
```

##### 422 Client Error
**Response:**
```json
{
  "status": 422
  "detail": "Room capacity -5 can not be lower than 1"
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
      "capacity": 2,
      "hotel": null,
      "hotelId": 3,
      "id": 1,
      "reservations": [],
      "roomNumber": "asd-3"
  },
  {
      "capacity": 2,
      "hotel": null,
      "hotelId": 3,
      "id": 2,
      "reservations": [],
      "roomNumber": "asd"
  },
  {
      "capacity": 2,
      "hotel": null,
      "hotelId": 1,
      "id": 3,
      "reservations": [],
      "roomNumber": "101"
  }
]
```

---

### Get a Specific Room
**Endpoint:** `GET /Api/Rooms/{roomId}`

#### Response Codes
##### 200 Success
**Response:**
```json
{
  "capacity": 2,
  "hotel": {
      "address": "124 Main St, New York, NY",
      "id": 3,
      "name": "The Grand",
      "rooms": [
          null
      ]
  },
  "hotelId": 3,
  "id": 4,
  "reservations": [],
  "roomNumber": "105"
}
```

##### 404 Not Found
**Response:**
```json
{
  "status": 404,
  "detail": "Room 5000 could not be found"
}
```

---

### Update a Room
**Authentication**: Bearer token
**Authorization**: Admin
**Endpoint:** `PUT /Api/Rooms/Update`

#### Response Codes
##### 200 Success
**Request:**
```json
{
  "capacity": 3,
  "hotelId": 1,
  "id": 24,
  "reservations": [],
  "roomNumber": "T2"
}
```

**Response:**
```json
{
    "capacity": 3,
    "hotel": {
        "address": "124 Main St, New York, NY",
        "id": 1,
        "name": "The Grand New York",
        "rooms": [
            null
        ]
    },
    "hotelId": 1,
    "id": 24,
    "reservations": [],
    "roomNumber": "T2"
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
            "JSON deserialization for type 'HotelManagementSystem.Interfaces.Dto.Room' was missing required properties, including the following: hotelId"
        ],
        "room": [
            "The room field is required."
        ]
    },
    "traceId": "00-6599e2605db8576fa895cdd10fb765d0-fc84825fa0a86b93-00"
}
```

...

##### 422 Client Error (Continued)
**Response:**
```json
{
  "status": 422,
  "detail": "Room capacity -1 can not be lower than 1"
}
```

##### 404 Not Found
**Response:**
```json
{
  "status": 404,
  "detail": "Room 200 could not be found"
}
```

---

### Delete a Room
**Authentication**: Bearer token
**Authorization**: Admin
**Endpoint:** `DELETE /Api/Rooms/{roomId}`

#### Response Codes
##### 204 No Content
**Response:** _(No Content)_

##### 404 Not Found
**Response:**
```json
{
  "status": 404,
  "detail": "Room 200 could not be found"
}
```

---

## Users API

### Register a User
**Endpoint:** `POST /Api/Users/Register`

#### Response Codes
##### 204 No Content
**Request:**
```json
{
  "email": "user2@test.com",
  "name": "user2",
  "surname": "user2",
  "password": "Test@1234",
  "passwordConfirmation": "Test@1234",
  "username": "user2"
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
            "The JSON object contains a trailing comma at the end which is not supported in this mode. Change the reader options. Path: $ | LineNumber: 6 | BytePositionInLine: 0."
        ],
        "request": [
            "The request field is required."
        ]
    },
    "traceId": "00-73da065a77d15f5a60045be04529e3df-3acdc5d3a4c10671-00"
}
```

##### 422 Client Error
**Response:**
```json
{
  "status": 422,
  "detail": "User user2 already exists."
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
  "password": "Test@1234",
  "username": "test"
}
```

**Response:**
```json
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIxYjVhMDhkYi1iOWQ0LTQ2MWEtOGVhZC1hYjlmYmJkMDhiNjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJzdWIiOiI5Y2QzOWQ2NC0wMjczLTRiMzQtODcwNS01YWJkNGY2NTIzMTMiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW4iLCJVc2VyIl0sImV4cCI6MTczNDY1NTk2MCwiaXNzIjoiSG90ZWxNYW5hZ2VtZW55U3lzdGVtIiwiYXVkIjoiVHJ1c3RlZENsaWVudCJ9.BVkmMB-9r133JKUKB3BZjwXs4aWiiGuKqovwNJw8-44"
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

### Refresh token
**Endpoint:** `POST /Api/Users/Refresh`

#### Response Codes
##### 200 Success
**Response:**
```json
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI0OTZiNmFiNi0xMmExLTQzNWQtYTVlMy00NjcwMjJlNDkwNGYiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJzdWIiOiI5Y2QzOWQ2NC0wMjczLTRiMzQtODcwNS01YWJkNGY2NTIzMTMiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW4iLCJVc2VyIl0sImV4cCI6MTczNDY1NjAwMSwiaXNzIjoiSG90ZWxNYW5hZ2VtZW55U3lzdGVtIiwiYXVkIjoiVHJ1c3RlZENsaWVudCJ9.0Z5Vv1Zv2Fyl3yxEp5kvahaxd3QtYY2mTbGt1VeE6PU"
}
```

---

### Get current user
**Authentication**: Bearer token
**Authorization**: User, Admin
**Endpoint:** `GET Api/Users`
#### Response Codes
##### 200 Success
**Response:**
```json
{
    "email": "admin@admin.com",
    "id": "9cd39d64-0273-4b34-8705-5abd4f652313",
    "name": "admin",
    "surname": "admin",
    "roles": [
        "Admin",
        "User"
    ],
    "username": "admin"
}
```

---

### Filter users
**Authentication**: Bearer token
**Authorization**: Admin
**Endpoint:** `PUT /Api/Users/Filter`

#### Response Codes
##### 200 Success
**Request:**
```json
{
    "name": "user"
}
```

**Response:**
```json
[
    {
        "email": "user@user.user",
        "id": "928e0c0c-edcb-4a75-8633-3d9ec0889342",
        "name": "user",
        "surname": "user",
        "reservations": [],
        "roles": [
            "User"
        ],
        "username": "user"
    },
    {
        "email": "newUser@user.user",
        "id": "5de77e5a-ebb9-405f-9122-798302d206aa",
        "name": "user",
        "surname": "user",
        "reservations": [],
        "roles": [
            "User"
        ],
        "username": "newUser"
    },
    {
        "email": "user2@test.com",
        "id": "148532ea-4e02-40cc-af7c-0d5f84f3f7a0",
        "name": "user2",
        "surname": "user2",
        "reservations": [],
        "roles": [
            "User"
        ],
        "username": "user2"
    }
]
```
