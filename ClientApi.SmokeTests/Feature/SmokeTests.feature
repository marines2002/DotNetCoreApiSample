Feature: SmokeTests
	In order validate Api has been deployed correctly
	As an Api Consumer
	I want to successfully call the Api endpoints 

@Smoke
Scenario: Call ECS Get Client Endpoint Should Return OK
	Given I am using a HttpClient
	When I call valid Client get endpoint
	Then the correct response code 'OK' is returned
	And some clients are returned
