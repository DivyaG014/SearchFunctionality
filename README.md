# SearchFunctionality

This is a simple .Net Core 6 API for finding flights based on boarding airport and destination airport. It uses the SQL server as Database.

# API Endpoints

**POST** `api/Authentication/login` <br>
This is the Authntication endpoint. It requires to send username and password for login and returns a JWT token and its expiry details. 
Response:-
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiZGl2eWEuZ3VwdGFAZ21haWwuY29tIiwianRpIjoiY2JkOGM2MDItOTE3MS00OWEyLTllMzAtMDcyZmJlMzBiOThmIiwiZXhwIjoxNzA4NzA5NDYwLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjYwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MjAwIn0.zlJ5FE3YRVfu4FaZRzX-QJo2ryJ28zXybFaiUo3mChI",
  "expiration": "2024-02-23T17:31:00Z"
}
```

**GET** `api/Airport/GetAllAirports` <BR>
This Endpoint can be called on the search page to get the list of all the Airports to show in From and To Dropdowns. The response of this endpoint has the name and code of the airport in JSON format. The code of the airports needs to be sent when calling the search api.

Response:-
```json
[
  {
    "airportName": "Hyderabad Airport",
    "airportCode": "HYD0012"
  },
  {
    "airportName": "Delhi International Airport",
    "airportCode": "DEL0023"
  },
  {
    "airportName": "Banglore International Airport",
    "airportCode": "BANG990"
  },
  {
    "airportName": "Mumbai International Airport",
    "airportCode": "MUM0078"
  },
  {
    "airportName": "Indore Airport",
    "airportCode": "INDB3342"
  }
]

```
**POST** `api/Flights/GetFlights` <br>
This Endpoint searches the flight based on To and FROM detail and other filters like Price and a open search text. The Reuest model is as follows:- <br>

```json
{
  "fromAirportCode": "HYD0012",
  "toAirportCode": "DEL0023",
  "fromPrice": 0,
  "toPrice": 0,
  "searchText": ""
}
```

The Response contains information about the Flights in JSON format.

``` json
[
  {
    "flightNumber": "FE1123",
    "fromAirport": "Hyderabad Airport",
    "toAirport": "Delhi International Airport",
    "arrivalTime": "2024-03-10T20:18:30.3",
    "departureTime": "2024-03-10T20:18:30.3",
    "price": 12000
  },
  {
    "flightNumber": "FE1255",
    "fromAirport": "Hyderabad Airport",
    "toAirport": "Delhi International Airport",
    "arrivalTime": "2024-03-10T20:18:30.3",
    "departureTime": "2024-03-10T20:18:30.3",
    "price": 13896
  }
]
```

#Models

Registered users

| Column name | Type |
|--|--|
| Id | integer |
| UserName | string |
| FirstName | string |
| LastName | string |
| LoginPassword | string |

Flight

| Column name | Type |
|--|--|
| Id | integer |
| FromAirportId | integer |
| ToAirportId | integer |
| FlightNumber | string |
| DepartureTime | datetime |
| ArrivalTime | datetime |
| Price | long |


Airport

| Column name | Type |
|--|--|
| Id | integer |
| AirportName | string |
| Code | string |

