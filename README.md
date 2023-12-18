# dotnet-code-challenge-jk

Project created using the .NET 6 code challenge.

- Added a `GET` route for getting the number of nested reports per employee.
- Added a DbContext, Repository, Service, and Controller for creating and getting Compensation records.
- Added comments for created CompensationService and CompensationRepository interfaces.
- Added relevant tests for Compensation functionality to test each possible outcome from the routes.
- Created the CompensationSeedData.json to load in database records for testing.

## `GET /api/compensation`

### Request

```json
{
	"employee": {
		"employeeId": "16a596ae-edd3-4847-99fe-c4518e82c86f",
		"firstName": "John",
		"lastName": "Lennon",
		"position": "Development Manager",
		"department": "Engineering",
		"directReports": null
	},
	"salary": 100000
}
```

### Response

```json
{
	"compensationId": "d89fc6e6-46a2-4584-8cde-a04242bddab1",
	"employee": {
		"employeeId": "16a596ae-edd3-4847-99fe-c4518e82c86f",
		"firstName": "John",
		"lastName": "Lennon",
		"position": "Development Manager",
		"department": "Engineering",
		"directReports": null
	},
	"salary": 100000,
	"effectiveDate": "2023-12-18T17:54:54.3943489-05:00"
}
```

## `GET /api/compensation/employee/{id}`

### Response

```json
{
	"employee": {
		"employeeId": "16a596ae-edd3-4847-99fe-c4518e82c86f",
		"firstName": "John",
		"lastName": "Lennon",
		"position": "Development Manager",
		"department": "Engineering",
		"directReports": []
	},
	"salary": 100000,
	"effectiveDate": "2023-12-18T17:54:54.3943489-05:00"
}
```
