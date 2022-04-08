#_*_coding: utf-8_*_

from selenium import webdriver
import time
import sys
import os
import requests
import urllib3
from googletrans import Translator, constants
from datetime import datetime

def recentRelease(driver = webdriver, driveow = False):
    recRel = []
    # Optional argument, if not specified will search path.
    if (not driveow):
        driver = webdriver.Chrome("C:\\Users\\Bob\\Downloads\\chromedriver_win32 (1)\\chromedriver.exe")
    driver.minimize_window()
    
    #open wcofun through an emulated selenium browser
    driver.get('https://www.wcostream.com/last-50-recent-release');
    
    #get the html of it (takes a few seconds... hopefully)
    html = driver.page_source
    
    #exit the browser
    if (not driver):
        driver.quit()

    #parse all of the new releases out of the html
    lani2 = 0
    while html.find('...">', lani2) > 0:
        lani = html.find('...">', lani2) + 6
        lani2 = html.find('</a>', lani)
        recRel.append(html[lani:lani2])
    return recRel, html
    

def loadAnis(driver = webdriver, driveow = False):
    # Optional argument, if not specified will search path.
    if (not driveow):
        driver = webdriver.Chrome("C:\\Users\\Bob\\Downloads\\chromedriver_win32 (1)\\chromedriver.exe")
    driver.minimize_window()
    #open wcofun through an emulated selenium browser
    driver.get('http://www.wcofun.com//subbed-anime-list');
    
    #get the html of it (takes a few seconds... hopefully)
    html = driver.page_source
    
    #exit the browser
    if (not driver):
        driver.quit()
    
    #get the table of animes from the html
    p1 = html.find('class="sep">#</p><ul>') + 22
    p2 = html.find('<div class="fifteen columns">') - 54
    anis = html[p1:p2]
    
    #parse all of the animes from the table and put it into a list
    animes = []
    lani2 = 0
    while anis.find('" title="', lani2) > 0:
        lani = anis.find('" title="', lani2) + 9
        lani2 = anis.find('">', lani)
        animes.append(anis[lani:lani2])

    print(str(len(animes)) + " animes loaded into memory.")
    return animes, anis

def convertToHTML(animes, anis, epis = False, rating = False):
    #take the list of found animes and links and turn it into an HTML page
    #also open that page automatically
    #this makes it easier to browse the animes
    print("loading HTML...")
    htmlFile = open(r"mangaDoc.html", "w")
    htmlFile.write("<!DOCTYPE html>\n<html>\n<body>")

    # datetime object containing current date and time
    now = datetime.now()
    htmlFile.write("<p><h1>Page Generated on: " + now.strftime("%B %d, %Y %H:%M") + " EST</h1></p>")
    link = ""
    imghtml = ''
    for anime in animes:
        #get the link of the anime from the anis HTML
        lani = 0
        lani2 = 0
        if (epis == True):
            lani2 = anis.find(anime) - 30
            lani = anis.rfind("https://www.", 0, lani2)
        else:
            lani2 = anis.find(anime) - 9
            lani = anis.rfind(".com/anime/", 0, lani2) - 18
        if (lani2 != -1 and lani != -1 and lani < lani2):
            link = " href='" + anis[lani:lani2] + "' target='_blank' rel='noopener noreferrer'>"
            if (epis == True):
                imghtml = '<img src="https://cdn.animationexplore.com/thumbs/'
                aniwebname = anis[lani + 26:lani2] #get web name of episode
                imghtml = imghtml + aniwebname 
                imghtml = imghtml + '.jpg" width="213" height>\n'
            elif (len(animes) > 0):
                #get first photo link from google images lmao
                url = "https://www.google.com/search?q=" + anime + "&tbm=isch"
                r = requests.get(url)
                code = (r.content).decode("latin-1")
                p1 = code.find('class="yWs4tf"')
                p2 = code.find('"/>', p1+32)
                imghtml = '<img src="' + code[p1+27:p2] + '">\n'
            else:
                imghtml = ''
        else: #error handling
            print("error retrieving link: " + anime)
            print(lani)
            print(lani2)
            print(anis[lani:lani+50])
            print(anis[lani2-50:lani2])
            link = ">"
            print("<p><a" + link + anime + "</a></p>\n")
        if (rating):
            htmlFile.write("<p><a" + link + anime + "</a>" + " (" + getRating(anime[0:-15]) + ")" + "</p>\n")
        else:
            htmlFile.write("<p><a" + link + anime + "</a></p>\n")
        htmlFile.write(imghtml)
    htmlFile.write("</body>\n</html>")
    htmlFile.close()
    print("wrote " + str(len(animes)) + " animes to doc")
    os.system('mangaDoc.html') #open the page automatically
    
def getRating(anime):
    if (anime == ""):
        return ""
    anime.replace(' ', '+')
    url = "https://www.google.com/search?q=" + anime
    r = requests.get(url)
    code = (r.content).decode("latin-1")
    p1 = code.find('<span aria-hidden="true" class="oqSTJd">')
    p2 = code.find('10', p1+40)
    if (p2-(p1+40) > 7 and code.find("solving the above CAPTCHA will let you continue") == -1):
        print(p1,p2)
        print(code)
        return code[p1+40:p1+46]
    elif (p1 == -1):
        return "Rating not found."
    else:
        votep1 = code.find('<span>(', p2)
        votep2 = code.find(')', votep1)
        return code[p1+40:p2+2] + " with " + code[votep1 + 7 : votep2] + " votes" #return rating along with num votes

def translate(text, to = 'ja'):
    translator = Translator()
    ttext = translator.translate(text, dest="ja").pronunciation
    ttext = ttext.replace("ī", "ii")
    ttext = ttext.replace("ō", "oo")
    ttext = ttext.replace("ā", "aa")
    while ttext[0:1] == " ":
        ttext.replace(" ", '', 1)
    return ttext

print("input anime to search.")
print("type rec rel to recieve the recent releases.")
print("type EXIT to exit.")
print(".! gets rating\n/ gets link\n## gets HTML page\n~translates your english search into Japanese automatically\nrec rel gets recent releases\nrec subs gets recently uploaded subbed anime")
print("cannot combine: ratings with recents, / with ##")
print("order: ## then .! then ~ and combine by taking them out")
loaded = False
recLoaded = False
search = input()
animes = []
recRel = []
recSubl = []
html = ""
rechtml = ""
while search != "EXIT":
    cmd = False
    recent = False
    loadHTML = False
    incRating = False
    if (search[0:1] == '/'):
        cmd = True
        search = search.replace('/', '', 1)
        print("returning link with search_name: " + search)

    #recent releases
    if (search.lower() == "rec rel"):
        if (not recLoaded):
            #call recentRelease function
            recRel, rechtml = recentRelease()
            recLoaded = True
        for ani in recRel:
            print(ani)
        search = input()
        continue

    #convert list to HTML hyperlink list
    if (search[0:2] == '##'):
        loadHTML = True #flag HTML
        search = search.replace('#', '', 2)
        print("Converting search to HTML page: " + search)

    #include ratings within the search
    if (search.find(".!") != -1 and search.find(".!") < 6):
        incRating = True
        search = search.replace(".!", "", 1)
        print("Adding rating to search")
        
    if (search[0:1] == '~'):
        search = search.replace('~', '', 1)
        search = translate(search)
        print("search_name: " + search)

    #recent subs
    if (search.lower() == "rec subs"):
        if (not loaded and not recLoaded):
            #this method saves on time, as it uses the same web driver for both of them (hopefully)
            driver = webdriver.Chrome("C:\\Users\\Bob\\Downloads\\chromedriver_win32 (1)\\chromedriver.exe")
            animes, anis = loadAnis(driver, True)
            loaded = True
            recRel, rechtml = recentRelease(driver, True)
            recLoaded = True
            driver.quit()
            
        if (not loaded):
            animes, anis = loadAnis()
            loaded = True
        if (not recLoaded):
            recRel, rechtml = recentRelease()
            recLoaded = True
        #output only the recently released subs
        recSubl.clear()
        for ani in recRel:
            #if the word 'Dubbed' is in the title then throw it out immediately
            if (ani.find("Dubbed") > -1):
                continue
            #find if the cartoon is in the list of subbed animes
            recSubs = len(list(filter(lambda anim: (anim.lower().find(ani[0:ani.find(" Episode")].lower()) > -1 or anim.lower().find(ani[0:ani.find(" Season")].lower()) > -1 or anim.lower().find(ani[0:ani.find(" Final Season")].lower()) > -1), animes)))
            if (recSubs > 0):
                recSubl.append(ani)
                print(ani)
        if (loadHTML): #convert to HTML if the flag has been set off using the '##rec subs' command
            convertToHTML(recSubl, rechtml, True)
        search = input()
        continue
    
    #match search to the list
    #load anis if not already
    if (not loaded):
        animes, anis = loadAnis()
        loaded = True
    
    foundAnis = list(filter(lambda ani: (ani.lower().find(search.lower()) > -1), animes))
    print(str(len(foundAnis)) + " animes found:")
    if (incRating and not loadHTML):
        print("Ratings:")
        for fani in foundAnis:
            print(fani + ": " + getRating(fani[0:-15]))
    else:
        for fani in foundAnis:
            print(fani)
    if (cmd and len(foundAnis) == 1):
        #get the link of the anime from the anis HTML
        lani2 = anis.find(foundAnis[0]) - 9
        lani = anis.rfind(".com/anime/", 0, lani2) - 18
        if (lani2 != -1 and lani != -1 and lani < lani2):
            print(anis[lani:lani2])
        else:
            print("error retrieving link:")
            print(lani)
            print(lani2)
            print(anis[lani:lani+50])
            print(anis[lani2-50:lani2])
    if (loadHTML and len(foundAnis) > 0):
        convertToHTML(foundAnis, anis, False, incRating)
        
    search = input()
    while (search == "" or search == "/" or search == "##"):
        search = input()
    
