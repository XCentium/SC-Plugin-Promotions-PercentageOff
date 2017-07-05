
Sitecore Commerce Engine Percentage Off Promotions plugin
======================================

This plugin allows the set a percentage off on a product or service during Promotions. 
- It is very easy to integrate or extend to fit your needs.
- If will take the set percentage value off the list price if percentage value is set.
- It it can be set to either work with other promotions and coupons or to only work alone bt setting the value of "CombineWithOtherPromos" in its policy config to either true or false;


Grouping
========
This is a Promotions plugin

Sponsor
=======
This plugin was sponsored and created by XCentium.

How to Install
==============

1. Copy it to your Sitecore Commerce Engine Solution and add it as a project 

2. Add it as a dependency to your Sitecore Commerce Engine Project.Json file by adding the line below
    "Plugin.Xcentium.Promotions.PercentOff": "1.0.2301"

3. Add the settings below to Sitecore Commerce Engine's environment json files

```
        {
          "$type": "Plugin.Xcentium.Promotions.PercentOff.PercentOffClientPolicy, Plugin.Xcentium.Promotions.PercentOff",
          "PercentOffFieldNameInSitecore": "Percentage Off",
          "CombineWithOtherPromos": false,
          "PolicyId": "PercentOffClientPolicy",
          "Models": {
            "$type": "System.Collections.Generic.List`1[[Sitecore.Commerce.Core.Model, Sitecore.Commerce.Core]], mscorlib",
            "$values": [
            ]
          }
        },

```
4. Add a field to your commerce product template which you will use to set the value of percentage off when you want to put on a promo for a product. You may name the field as you like.

5. Change the setting for "PercentOffFieldNameInSitecore" to the field name.

6. Change the setting for "CombineWithOtherPromos" to true or false depending on weather you want this to work with other promos or be exclusive.

7. Bootstrap your commerce engine so that the new json policy gets loaded.

You are ready to start using it. 

Note:
=====
- Each physical product that will be shipped to buyers should have dimensions and weight.
- You may need to extend your commerce product template either directly in Sitecore or by using Catalog & Inventory Schema Manager tool and add the additional text field (Percentage Off)
- If the field is named differently from "Percentage Off", you will need to set the actual name in the json policy config above changing the values of:

```
        "PercentOffFieldNameInSitecore": "WeiPercentage Offght",

```

- If you have any questions, comment or need us to help install, extend or adapt to your needs, do not hesitate to reachout to us at XCentium.




