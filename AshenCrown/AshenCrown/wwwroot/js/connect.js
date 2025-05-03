//init conn
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

// 
connection.on("loadCount", count => {
    document.getElementById("totalDisplay").innerText = count;
});

//
function newLoad() {
    connection.invoke("CountGaming");
}

// start connect
function fulfill() {
    console.log("toi da thanh cong");
    newLoad();
}
function reject() {

}
connection.start()
    .then(fulfill, reject);

