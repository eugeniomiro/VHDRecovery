##README

One of my VHD disks got corrupted when running in a 3Tb USB faulty drive when running an XP machine under Oracle Virtual Box.
I started surfing the internet for a solution I don't have to pay money for it (I actually don't have it) so I started with [this
post](http://social.technet.microsoft.com/Forums/windowsserver/en-US/48428817-86a7-418c-9633-2ae405593622/how-to-fix-a-corrupted-hyperv-vhd-file?forum=winserverhyperv) 
which in turn took me to [this other blog](http://blogs.technet.com/b/tonyso/archive/2011/12/06/how-to-fix-a-corrupted-hyper-v-vhd-file.aspx) 
which didn't help much but pointed me to the [VHD tool](http://archive.msdn.microsoft.com/vhdtool) that, after not helping much 
either pointed me to the VHD Specification (that wasn't in that link but it existed finally [here](http://www.microsoft.com/en-us/download/details.aspx?id=23850).

Then I downloaded it and started this solution today, Monday, February 17 2014, 11:15 PM, together with this README file which is going to be kind of 
a diary of the project.

After reading the first two pages I found the Dynamic Hard Disk Image Footer, which I aim to read with the first tool I'll write
* VHDHdr