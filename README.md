# ProvisioningPnp
Provisioning with PNP in C#

## COMMANDS TO LAUNCH
Use this action to review help about the different type of actions:
> --action help

Use this action to generate a Template to Upload with the following commands:
> --action export --username jesus.velasco@tenant.onmicrosoft.com --password *** --urlSite https://tenant.sharepoint.com/

Use this action to apply a template to site with the following commands:
> --action import --username jesus.velasco@tenant.onmicrosoft.com --password *** --urlSite https://tenant.sharepoint.com/ --templateName manifest

## PARAMS
The params that can be use, be the following:
 - **action**: action to be do it
 - **username**: login email of the admin user
 - **password**: password of the admin user
 - **urlSite**: url of the site where is to be logged and import/export the template
 - **templateName** *(No required (Only import))*: name of the file, without extension, it must be a XML file and a valid template
 - **dirName** *(No required)*: Path of directory when the file can be found

The directory must to have a folder with the name provision where inside is the template XML
