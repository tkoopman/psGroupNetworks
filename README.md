# psGroupNetworks

## Overview
PowerShell module for sorting a list of IPv4 & IPv6 subnets and removing any duplicates or overlapping entries.

## Example
```powershell
# First get large list of Subnets
$O365IPAddresses = "https://support.content.office.net/en-us/static/O365IPAddresses.xml";
[XML]$O365 = Invoke-WebRequest -Uri $O365IPAddresses -DisableKeepAlive;

$O365IPs =  $O365.products.product.addresslist |
  Where-Object { ($_.type -eq "IPv4") -or ($_.type -eq "IPv6") } |
  Select-Object -ExpandProperty address -ErrorAction SilentlyContinue
            
# Now lets sort
$O365IPs | Group-Networks -Verbose
```
