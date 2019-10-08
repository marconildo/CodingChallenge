Feature: Calculate Number of Stops Required
	to avoid fuel shortages and travel a specific distance
	calculate how many refueling stops are required.

Scenario: Listing of all starships and their number of stops necessary
	Given I have a desire to know how many stops all starships have to make in a flight of 1000000 MGLT
    When I press enter
    Then the calculator must list all spaceships and their number of stops before depleting all consumables like this:
      | Name                          | Stops   |
      | Millennium Falcon             | 9       |
      | Y-wing                        | 74      |
      | Rebel transport               | 11      |