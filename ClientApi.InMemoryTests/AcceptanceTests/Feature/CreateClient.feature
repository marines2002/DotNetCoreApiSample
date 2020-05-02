Feature: CreateClient
	In order validate Api Functionality
	As an Api Consumer
	I want to successfully call Api endpoints 

@Integration
Scenario: Call ECS Client Post Endpoint Should Return OK
	Given I am using TestServer
	And I call valid Client post endpoint with client with project id '12345678'
	When I call valid Client get endpoint with projectId '12345678'
	Then the correct response code 'OK' is returned
