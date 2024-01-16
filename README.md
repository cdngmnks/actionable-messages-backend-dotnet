# actionable-messages-backend-dotnet

This is a proof of concept for Universal Actions Model based on [Microsoft Documentation](https://learn.microsoft.com/en-us/outlook/actionable-messages/adaptive-card-expense-approval-sample?tabs=mobile).

The base idea in this proof of concept is to send an action with the new Action.Execute action and refresh the adaptive cards in outlook with the selected answer.

To do this we need 3 things:
1. Send our card to our outlook with the help of [Actionable Messages Designer](https://amdesigner.azurewebsites.net)
2. Setup an azure bot
3. Setup an api that will use bot framework 4 to handle the actions.

# Actionable Messages Designer 
For this to work properly adaptive card version 1.4 or bigger should be used. In this proof of concept 1.4 is used. The cards used for this poc are in this repository.
Also in every adaptive card that you send trough the designer,an originator must be set.
Don't forget to set a [provider](https://outlook.office.com/connectors/oam/publish) in outlook. For testing purposes use Test Users scope.

# Setup an azure bot
On azure select a Azure Bot service and setup the configuration. 
First setup the Messaging endpoint. This should be the enpoint where the api runs. If you want to debug you api, ngrok url can be used.
We can use multiple types of azure bot. This is tested on two types UserAssigned and MultiTenant. For testing on localhost with ngrok MultiTenant bot is needed because of the authentication.
Also the outlook channel should be opened and here we can register the provider with the bot id and not with url.

# Setup an api
For this poc bot framework 4 empty template is used. The code handles OnAdaptiveCardInvokeAsync method and the action is processed based on the verb.
The appsettings.json should be set which are given in the exapmle.appsettings.json file. There a two types of settings depending on what type of azure bot, UserAssigned or MultiTenant.
For localhost testing only MultiTenant works with ngrok. When the api is deployed don't forget to change the Messaging endpoint in the azure bot.
If the api is published on app service on azure and the UserAssigned bot is used, UserAssigned should be addded in the service propeties. 

