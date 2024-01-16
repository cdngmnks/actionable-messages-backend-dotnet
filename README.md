# Adaptive Cards - Universal Action Model

This is a proof of concept for Universal Actions Model based on [Microsoft Documentation](https://learn.microsoft.com/en-us/outlook/actionable-messages/adaptive-card-expense-approval-sample?tabs=mobile).

The base idea in this proof of concept is to send an action with the new Action.Execute action and refresh the adaptive cards in outlook with the selected answer.

To do this we need 3 things:
1. Send our card to our outlook with the help of [Actionable Messages Designer](https://amdesigner.azurewebsites.net)
2. Setup an azure bot
3. Setup an api that will use bot framework 4 to handle the actions.

## Actionable Messages Designer 
For this to work properly adaptive card version 1.4 or bigger should be used. In this proof of concept 1.4 is used. The cards used for this poc are in this repository.
Also in every adaptive card that you send trough the designer,an originator must be set.
Don't forget to set a [provider](https://outlook.office.com/connectors/oam/publish) in outlook. For testing purposes use Test Users scope.

## Setup an azure bot
On azure select a Azure Bot service and setup the configuration. 
First setup the Messaging endpoint. This should be the enpoint where the api runs. If you want to debug you api, ngrok url can be used.
We can use multiple types of azure bot. This is tested on two types UserAssigned and MultiTenant. For testing on localhost with ngrok MultiTenant bot is needed because of the authentication.
Also the outlook channel should be opened and here we can register the provider with the bot id and not with url.

## Setup an api
For this poc bot framework 4 empty template is used. The code handles OnAdaptiveCardInvokeAsync method and the action is processed based on the verb.
The appsettings.json should be set which are given in the exapmle.appsettings.json file. There a two types of settings depending on what type of azure bot, UserAssigned or MultiTenant.
For localhost testing only MultiTenant works with ngrok. When the api is deployed don't forget to change the Messaging endpoint in the azure bot.
If the api is published on app service on azure and the UserAssigned bot is used, UserAssigned should be addded in the service propeties. 

## Step by step setup
- Download and install [bot framework v4 templates for visual studio](https://marketplace.visualstudio.com/items?itemName=BotBuilder.botbuilderv4)
- Create a new project from the empty bot template.
- Create "Bots" folder and rename your EmptyBot.cs into TermsBot.cs. Move the bot into the folder.
- In Startup.cs change the line "services.AddTransient<IBot, EmptyBot>();" into "services.AddTransient<IBot, TermsBot>();"
- Clean the TermsBot.cs file and implement the OnAdaptiveCardInvokeAsync method. Inside this method you can handle actions based on verb.
- Download ngrok, open cmd in the map where ngrok.exe is positioned. Run the following command "ngrok http {port} --host-header rewrite". Copy the url provided by ngrok.
- Create multitenant Azure Bot service, configure the azure bot with Messaging endpoint value "{ngrok url}/api/messages".
- Copy the MicrosoftAppType, MicrosoftAppId and MicrosoftAppPassword and paste it in the appsettings.json file in the project. The setting keys are already preset by the template, only te values should be copied. For MultiTenant bot type the MicrosoftAppTenantId key should be empty.
- On the Azure bot go to the channels tab and add the outlook channel. When you click on the outlook channel a vew is opened wuth two tabs. I the Actionable Messages tab there is a line that says "You need to be registered as a partner for Outlook Actionable Messages, please register here." Click on the link and register a provider, the bot id will be automatically set. Choose TestUsers and set your email test users. Save the provider.
- Go to [Actionable Messages Designer](https://amdesigner.azurewebsites.net), but use the v1.4, and send the example card in this project or use your card. Send it to your test email. 
- Run your project on the port that you set in ngrok command.
- Set a debug point in the BotController file and click on an action in the received action card in outlook.
- When you are sure that your project works like you needed it to, create an app service in azure and publish your project to azure.
- Change the Messaging endpoint value into your azure app service url. Example universalactionmodel.azurewebistes.net/api/messages
- Click on an action and see if it works properly.
- Important!! In this code cache memory is used to save the choosen choice state. This is not a good practice and is used only to test the adaptive cards handling. In realty these states are saved in a database.

## References
- [Adaptive Card Templating](https://learn.microsoft.com/en-us/adaptive-cards/templating/)
- [Overview of Universal Action Model](https://learn.microsoft.com/en-us/outlook/actionable-messages/universal-action-model)
- [Universal Actions for Adaptive Cards](https://learn.microsoft.com/en-us/outlook/actionable-messages/universal-action-model)
- [Refresh an actionable message when the user opens it](https://learn.microsoft.com/en-us/outlook/actionable-messages/auto-invoke)
- [Create a basic bot](https://learn.microsoft.com/en-us/azure/bot-service/bot-service-quickstart-create-bot?view=azure-bot-service-4.0&tabs=csharp%2Cvs)
- [Create a an Azure Bot resource](https://learn.microsoft.com/en-us/azure/bot-service/abs-quickstart?view=azure-bot-service-4.0&tabs=userassigned)
- [Bot Framework Documentation](https://docs.botframework.com)
- [Bot Basics](https://docs.microsoft.com/azure/bot-service/bot-builder-basics?view=azure-bot-service-4.0)

## Issues
To view or log issues see [issues](https://github.com/cdngmnks/actionable-messages-backend-dotnet/issues).

## License
Copyright (c) codingmonkeys doo. All Rights Reserved. Licensed under the [MIT License](https://github.com/cdngmnks/actionable-messages-backend-dotnet/blob/main/LICENSE).
