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
