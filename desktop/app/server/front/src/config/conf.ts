const xmlHttpRequest = new XMLHttpRequest();
xmlHttpRequest.open("GET", "/conf.json", false);
xmlHttpRequest.send();
console.log(xmlHttpRequest.responseText);

type Config = {
    "endpoints": {
        "api": string,
        "socket-io": string
    }
}
let rawConf = JSON.parse(xmlHttpRequest.responseText);
export let conf: Config = rawConf.development;

if (process.env.NODE_ENV === "production") conf = rawConf.production;
