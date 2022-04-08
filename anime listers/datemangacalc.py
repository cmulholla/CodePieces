import requests
import os
import time
from datetime import date
from datetime import datetime  
from datetime import timedelta
from time import strptime
import numpy as np

def ddate(f_date, l_date): #use date(yyyy, m/mm, d/dd)
    return int((l_date - f_date).days)
def mtoint(month_name): #ex "Jan"
    return strptime(month_name, '%b').tm_mon


"""print("Name of series: ")
name = input()
print("directory to download to:")
direc = input()"""
print("URL of chapters (manganelo.com only, ex 'https://manganelo.com/manga/komisan_wa_komyushou_desu'):")
mainurl = input()
#print("# Chapter to start on:")
#chps = input()

"""if not os.path.exists(direc+"\\"+name):
    os.makedirs(direc+"\\"+name)"""

print('Beginning')

#get html code

r = requests.get(mainurl)
code = (r.content).decode("utf-8")
print(code)

#get first date
datelastfind = code.rfind('<span class="chapter-time text-nowrap" title="')
datelastfind2 = code.find(":", datelastfind)
fdate = code[datelastfind+46:datelastfind2-3]
#print(fdate)

dates = []

#put all the dates into the dates[] array
while datelastfind > 0:
    datelastfind = code.rfind('<span class="chapter-time text-nowrap" title="', 0, datelastfind)
    datelastfind2 = code.find(":", datelastfind)
    if code[datelastfind+46:datelastfind2-3] == fdate or datelastfind==-1: #ensure it's not the first date and that the date can be found
        continue
    else:
        on = len(dates)
        dates.append(code[datelastfind+46:datelastfind2-3])
        dates[on]=str(int(dates[on][-4:])) + ", " + str(mtoint(dates[on][:3])) + ", " + str(int(dates[on][4:6]))
        print(dates[len(dates)-1])
#find the days between the dates, store in ddates[]
ddates = []
for x in range(1, len(dates)): #eliminate any date that's out of order (the nazi sort)
    f_date = date(int(dates[x-1][:4]), int(dates[x-1][6:dates[x-1].rfind(",")]), int(dates[x-1][dates[x-1].rfind(",")+1:]))
    l_date = date(int(dates[x][:4]), int(dates[x][6:dates[x].rfind(",")]), int(dates[x][dates[x].rfind(",")+1:]))
    #print(dates[x])
    if ddate(f_date, l_date)<0:
        print(str(l_date) + " - " + str(f_date) + " = " + str(ddate(f_date, l_date)))
        print("removing: " + dates[x-1])
        dates.pop(x-1)
        x=x-1
    if x==len(dates)-1:
        break
        
totaldays = 0
l_date = date(1,1,1)
for x in range(1, len(dates)):
    f_date = date(int(dates[x-1][:4]), int(dates[x-1][6:dates[x-1].rfind(",")]), int(dates[x-1][dates[x-1].rfind(",")+1:]))
    l_date = date(int(dates[x][:4]), int(dates[x][6:dates[x].rfind(",")]), int(dates[x][dates[x].rfind(",")+1:]))
    #print(dates[x])
    ddates.append(ddate(f_date, l_date))
    print(dates[x] + " - " + dates[x-1] + " = " + str(ddates[len(ddates)-1]))
    totaldays = totaldays + ddate(f_date, l_date)
    
ddates.sort()
# First quartile (Q1) 
Q1 = np.percentile(ddates, 25, interpolation = 'midpoint')

# Third quartile (Q3)
Q3 = np.percentile(ddates, 75, interpolation = 'midpoint')

#median
med = np.percentile(ddates, 50, interpolation = 'midpoint')
  
# Interquaritle range (IQR) 
IQR = Q3 - Q1 

print("Q1: " + str(Q1) + " Q3: " + str(Q3) + " IQR: " + str(IQR))
print("high fence: " + str( 1.5*IQR + Q3))
print("low fence: " + str(-1.5*IQR + Q1))
print("total days: " + str(totaldays))
totaldays = totaldays / len(ddates)
print("avg days: " + str(totaldays))
print("median days: " + str(med) + "\n")
if l_date > datetime.now().date():
    l_date = datetime.now().date()
print("Expected day for the next update is: " + str((l_date + timedelta(days=int(med)))) + ", \nwhich is " + str(int(med)) + " days from " + str(l_date) + ", the last chapter update.")




