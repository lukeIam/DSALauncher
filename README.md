# DSALauncher
DSALauncher is a simple tool that allows yout to uickly open your `Das Schwarze Auge`/`The Dark Eye` pdfs at the right page.
With the companion userscript you can even directly jump from [wiki-aventurica.de](https://wiki-aventurica.de) to the right positions in your pdf files.  
The whole thing was written in under a day - so do not expect too much...

## HowTo use
#### Open a document
`[documentKeyword]` + `enter`  
![openDoc](https://user-images.githubusercontent.com/5115160/44875183-b87f6600-ac9d-11e8-8694-e7ed7d8abe5f.png)


#### Open document on specific page
`[documentKeyword] [page]` + `enter`  
`[documentKeyword][page]` + `enter`  
![openPage](https://user-images.githubusercontent.com/5115160/44875236-e2388d00-ac9d-11e8-901c-310c46c6b6c7.png)

#### Open document and start a search
`[documentKeyword] [searchKeyword]` + `enter`  
![openKeyword](https://user-images.githubusercontent.com/5115160/44875298-10b66800-ac9e-11e8-9193-22fc7ecc5377.png)

#### Open document from wiki-aventurica.de
Just install the userscript and click on the new links on page numbers  
![image](https://user-images.githubusercontent.com/5115160/44875817-8b33b780-ac9f-11e8-87c5-33b99beb3574.png)

#### Position 
`click` + `drag` 

#### Exit DSALauncher
`exit` + `enter` 

## HowTo install
1. Dowload the [latest release](https://github.com/lukeIam/DSALauncher/releases)
2. Unzip the files to a folder of your choice
3. Open the `settings.json` file in a editor

| Key                | Description                                                                                      | Example                                                                   |
|--------------------|-------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------|
| `PdfViewer`        | Path to executable of the pdf viewer.                                                           | `C:\\Program Files (x86)\\Adobe\\Acrobat Reader DC\\Reader\\AcroRd32.exe` |
| `PdfCommandPage`   | Arguments to open a page (`{0}`=`pdf path` `{1}`=`page number`)                                 | `/A page={1} \"{0}\"`                                                     |
| `PdfCommandSearch` | Arguments to trigger a search (`{0}`=`pdf path` `{1}`=`keyword`)                                | `/A search=\"{1}\" \"{0}\"`                                               |
| `PdfBasePath`      | Base path to make relative paths absolute                                                       | 'C:\\DSA'                                                                 |
| `AlwaysTop`        | If `true` the input field will always stay on top                                               | 'true'                                                                    |
| `InactiveOpacity   | `A value between `0` and `1` which determinates the opacity of the inactive input field         | `0.8`                                                                     |
| `WebserverActive   | `If `true` a small webserver will be started which allows the companion userscript to open pdfs | `true`                                                                    |
| `WebserverPort`    | The port the webserver should use                                                               | `7964`                                                                    |
| `Files`            | List of the documents you own - see next table how to define documents                           | `[ ... ]`                                                                 |

**Files definition:**

| Key      | Description                                                                  | Example                                  |
|----------|------------------------------------------------------------------------------|------------------------------------------|
| Keywords | List of keywords for this document (adding wiki-aventurica name is sugested) | `[ "Wege der Helden", "WdH", "Helden" ]` |
| PdfPath  | Path to your pdf file (can be absolute or relative to `PdfBasePath`)         | `Regelwerke\\Wege der Helden.pdf`        |
| Offset   | Offset for the page numer for this document                                  | `1`                                      |

4. Start `DSALauncher.exe`
5. [Optional] Install `DSALauncher.user.js` in your browser
