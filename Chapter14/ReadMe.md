# CitySelector Sample Project

> Congratulations, you have successfully created a new solution using a multi-project template!

## Some things you might want to check

1.	Open the Web.config file in the WebApi project
- Does the value of the 'myDBConnectionString' connection string setting match what you typed as user input during project creation?

2.	Right-click on the solution in Solution Explorer and select Properties
- Is the multiple startup projects option selected and is the corresponding action for both the WebApi and WPF project set to 'Start'?

3.	Try running / debugging the solution
- Did both the WebApi and WPF projects start up?
- Were there any problems launching the WPF form or did it work on the first try? 
    - Did you see your DB Connection value displayed on the web page for the API project?
    - If you made multiple selections in the dependent dropdowns on the CitySelector WPF form and clicked the OK button were your selections copied to the clipboard?
- If there were any issues running the solution:
    - First try restarting the debugging session. Sometimes a delay in starting the Web API can cause an issue.
    - Be sure the 'BaseUrl' setting in the App.config file of the WPF project is set properly (matches the URL of the WebApi when launched. Likewise, ensure the 'DataProvider' value is set to 'API'.

