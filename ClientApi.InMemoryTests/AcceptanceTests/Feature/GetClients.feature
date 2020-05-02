Feature: GetClients
	In order validate Api Functionality
	As an Api Consumer
	I want to successfully call Api endpoints 

@Integration
Scenario: Call ECS Get Client Endpoint Should Return OK
	Given I am using TestServer
	And Some Test Clients Exist
	When I call valid Client get endpoint
	Then the correct response code 'OK' is returned
	And the number of results returned is '0'
