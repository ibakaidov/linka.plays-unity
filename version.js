const fs = require('fs')


 
const content = fs.readFileSync(".\\ProjectSettings\\ProjectSettings.asset", 'utf-8')

const version = content.match(/bundleVersion:.(.*)/)[1];

const installer = fs.readFileSync(".\\installer.iss", 'utf-8').replace("$version", version)
fs.writeFileSync(".\\installer.iss", 'utf-8')
fs.writeFileSync('".\\build\\linka.plays\\version.json', JSON.stringify({
    version
}))