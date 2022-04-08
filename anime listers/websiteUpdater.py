#this is meant to run purely from a terminal with args.
#pull rec subs, upload to HTML with title format: "subbedReleases mm/dd/yy hh;mm"
#also pull the anime titles for the rec subs and upload to HTML with title format: "ratings mm/dd/yy hh;mm"

from selenium import webdriver
import requests
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

def convertToHTML(animes, anis, fname, epis = False, rating = False):
    #take the list of found animes and links and turn it into an HTML page
    #also open that page automatically
    #this makes it easier to browse the animes
    print("loading HTML...")
    htmlFile = open(fname+".html", "w")
    htmlFile.write("<!DOCTYPE html>\n<html>\n<body>")

    # datetime object containing current date and time
    now = datetime.now()
    htmlFile.write("<p><h1>Page Generated on: " + now.strftime("%B %d, %Y %H:%M") + " EST</h1></p>")
    link = ""
    imghtml = ''
    print(anis)
    for anime in animes:
        #get the link of the anime from the anis HTML
        lani = 0
        lani2 = 0
        if (epis == True):
            lani2 = anis.find(anime) - 30
            lani = anis.rfind("https://www.", 0, lani2)
        else:
            print("Searching for: " + anime)
            lani2 = anis.find(anime) - 9
            lani = anis.rfind(".com/anime/", 0, lani2) - 18
            if (lani2 == -1):
                lani2 = anis.find(anime + " English Subbed") - 9
                lani = anis.rfind(".com/anime/", 0, lani2) - 18
            
        
        if (epis == True and lani < lani2):
            imghtml = '<img src="https://cdn.animationexplore.com/thumbs/'
            aniwebname = anis[lani + 26:lani2] #get web name of episode
            imghtml = imghtml + aniwebname 
            imghtml = imghtml + '.jpg" width="213" height>\n'
        else:
            #get first photo link from google images lmao
            url = "https://www.google.com/search?q=" + anime + "&tbm=isch"
            r = requests.get(url)
            code = (r.content).decode("latin-1")
            p1 = code.find('class="yWs4tf"')
            p2 = code.find('"/>', p1+32)
            imghtml = '<img src="' + code[p1+27:p2] + '">\n'
            
        if (lani2 != -1 and lani != -1 and lani < lani2):
            link = " href='" + anis[lani:lani2] + "' target='_blank' rel='noopener noreferrer'>"
        else: #error handling
            print("error retrieving link: " + anime)
            print(lani)
            print(lani2)
            print(anis[lani:lani+50])
            print(anis[lani2-50:lani2])
            link = ">"
            imghtml = ''
            print("<p><a" + link + anime + "</a></p>\n")
        if (rating):
            if (not epis):
                htmlFile.write("<p><a" + link + anime + "</a>" + " (" + getRating(anime) + ")" + "</p>\n")
            else:
                htmlFile.write("<p><a" + link + anime + "</a>" + " (" + getRating(anime[0:-15]) + ")" + "</p>\n")
        else:
            htmlFile.write("<p><a" + link + anime + "</a></p>\n")
        htmlFile.write(imghtml)
    htmlFile.write("</body>\n</html>")
    htmlFile.close()
    print("wrote " + str(len(animes)) + " animes to doc")
    #os.system('mangaDoc.html') #open the page automatically


def getRating(anime):
    if (anime == ""):
        return "no anime found"
    
    anime.replace(' ', '+')
    url = "https://www.google.com/search?q=" + anime
    r = requests.get(url)
    code = (r.content).decode("latin-1")
    p1 = code.find('<span aria-hidden="true" class="oqSTJd">')
    p2 = code.find('10', p1+40)
    if (p2-(p1+40) > 7 and code.find("solving the above CAPTCHA will let you continue") == -1):
        print(p1,p2)
        #print(code)
        print("Captcha lock")
        return code[p1+40:p1+46]
    elif (p1 == -1):
        return "Rating not found."
    else:
        votep1 = code.find('<span>(', p2)
        votep2 = code.find(')', votep1)
        return code[p1+40:p2+2] + " with " + code[votep1 + 7 : votep2] + " votes" #return rating along with num votes

def fRecSubs(recRel, animes):
    recSubl = []
    for ani in recRel:
        #if the word 'Dubbed' is in the title then throw it out immediately
        if (ani.find("Dubbed") > -1):
            continue
        #find if the cartoon is in the list of subbed animes
        recSubs = len(list(filter(lambda anim: (anim.lower().find(ani[0:ani.find(" Episode")].lower()) > -1 or anim.lower().find(ani[0:ani.find(" Season")].lower()) > -1 or anim.lower().find(ani[0:ani.find(" Final Season")].lower()) > -1), animes)))
        if (recSubs > 0):
            recSubl.append(ani)
            print(ani)
    return recSubl


#first, load all of the anime and recently released anime.
driver = webdriver.Chrome("C:\\Users\\Bob\\Downloads\\chromedriver_win32 (1)\\chromedriver.exe")
animes, anis = loadAnis(driver, True)
recRel, rechtml = recentRelease(driver, True)
driver.quit()

#TODO: if an anime with with word "subbed" is not found in the list, then re-load the "anime" list w/ loadAnis
#also, this requires recent released to be found every single time

#next, find all recently subbed releases.
recSubl = fRecSubs(recRel, animes)

#next, convert the recent subs into an HTML webpage
convertToHTML(recSubl, rechtml, "subbedReleases", True)

#next, create the HTML page with just the recently released titles and ratings for those anime
#first, find just the titles for these anime
recTitles = []
for ani in recSubl:
    line = ani.find(" Final Season")
    if (line == -1):
        line = ani.find(" Season")
    if (line == -1):
        line = ani.find(" Episode")
    if (ani.find('☆') > -1):
        continue
    if (line > -1):
        recTitles.append(ani[0:line])
print(recTitles)

anime = "Genjitsu Shugi Yuusha No Oukoku"
print(anis)
lani2 = rechtml.find(anime) - 9
lani = rechtml.rfind(".com/anime/", 0, lani2) - 18
print("Link is: " + rechtml[lani:lani2])

#next, convert this list into HTML with ratings
convertToHTML(recTitles, anis, "recentRatings", False, True)






