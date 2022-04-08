import requests
import os
import time

print("Name of series: ")
name = input()
print("directory to download to:")
direc = input()
print("URL of chapter to start on (manganelo.com only):")
mainurl = input()
print("Chapter to start on (only affects the naming of the files):")
chps = input()

if not os.path.exists(direc+"\\"+name):
    os.makedirs(direc+"\\"+name)

print('Beginning manga download')
totalpages=0
chp=int(chps)
chp-=1
while True:
    chp+=1
    #chapter stuff
    
    r = requests.get(mainurl)
    code = (r.content).decode("utf-8")
    x=1
    lastfindjpg=0
    
    while True:
        #specific page in chapter stuff
        #find the next page
        lastfind = code.find('.com/mangakakalot/', lastfindjpg)
        lastfindjpg = code.find('.jpg', lastfind)
        baseurl = code[lastfind-20:lastfindjpg+4]
        url = baseurl
        
        if url == '':
            break
        
        #download it
        r = requests.get(url)
        
        fname=direc+"\\"+name+"\\"+name+str(chp)+"\\" + url[41:].replace("/", "-")
        
        if not os.path.exists(direc+"\\"+name+"\\"+name+str(chp)):
            os.makedirs(direc+"\\"+name+"\\"+name+str(chp))
            
        with open(fname, 'wb') as f:
            f.write(r.content)
        
        if os.path.getsize(fname) < 156:
            os.remove(fname)
            break
        else:
            print(url[41:]+": "+str(os.path.getsize(fname))+" bytes")
        totalpages+=1
        x+=1
        
    #find next chapter
    
    nextbutton = code.rfind("btn-next a-h")
    nextbuttonend = code.find("\">NEXT CHAPTER</a>", nextbutton)
    if nextbuttonend<0:
        print("done")
        print(str(totalpages) + " total pages downloaded")
        break
    mainurl = code[nextbutton+20:nextbuttonend]
    print(mainurl)
