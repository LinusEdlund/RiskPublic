
function placeSoldiers(players) {
    const layer4 = document.querySelector('#layer4');
    const allOldText = layer4.querySelectorAll('text');
    allOldText.forEach(textElement => {
        textElement.remove();
    });

    for (let i = 0; i < players.length; i++) {
        for (let key in players[i].ownedCountries) {
            const a = layer4.querySelector('#' + key);
            const bbox = a.getBBox();
            var cx = bbox.x + bbox.width / 2;
            var cy = bbox.y + bbox.height / 2;

            // Create circle element
            var circle = document.createElementNS("http://www.w3.org/2000/svg", "circle");
            circle.setAttribute("cx", cx);
            circle.setAttribute("cy", cy);
            circle.setAttribute("r", "10");
            circle.setAttribute("fill", players[i].color);
            circle.setAttribute("fill-opacity", "1");
            circle.setAttribute("id", "circle-" + key);
            circle.style.pointerEvents = "none"; // Ensure circle doesn't intercept click events

            // Create text element
            var text = document.createElementNS("http://www.w3.org/2000/svg", "text");
            text.setAttribute("x", cx);
            text.setAttribute("y", cy);
            text.setAttribute("text-anchor", "middle");
            text.setAttribute("alignment-baseline", "central");
            text.setAttribute("fill", "#fff");
            text.setAttribute("font-size", "14");
            text.setAttribute("id", "text-" + key);
            text.style.pointerEvents = "none"; // Ensure text doesn't intercept click events
            text.textContent = players[i].ownedCountries[key];

            a.style.fill = players[i].color;
            // Append circle and text elements to layer4
            // layer4.appendChild(circle);
            layer4.appendChild(text);
        }
    }
}

function getCountries() {
    let stringList = [];
    const c = document.querySelectorAll('#layer4 .country');
    c.forEach(x => stringList.push(x.id));
    return stringList;
}

function highlightActivPlayer(pos) {
    const person = document.querySelector(`#pos-${pos}`);
    person.style.width = '80%';
}

function highlightLand(player) {

    player.soldierPlacing.forEach(land => {
        const landName = land.landName;
        const pathLand = document.getElementById(landName);
        pathLand.style.fill = player.color;
        pathLand.style.stroke = 'red';
    });
}

function hoverLand(country) {
    const land = document.getElementById(country);
    land.classList.add('hover');
    /*land.style.filter = "brightness(50%)";*/
}

function hoverLeaveLand(country) {
    const land = document.getElementById(country);
    land.classList.remove('hover');
    // land.style.filter = "brightness(100%)";
}

function clickedLand(country) {
    const land = document.getElementById(country);
    land.classList.add('clicked');
    //land.style.filter = "brightness(50%)";
    //land.style.transform = "scale(1.005)";
}

function clickedLandRemove(country) {
    const land = document.getElementById(country);
    land.classList.remove('clicked');
    //land.style.filter = "brightness(100%)";
    //land.style.transform = "scale(1)";
}


function addSolders(country, amount) {
    const textElement = document.getElementById('text-' + country);
    const textContent = textElement.textContent;
    const textNumber = parseInt(textContent);
    const newNumber = textNumber + amount;
    textElement.textContent = newNumber;
}

function scrollToBottom(id) {
    var messageContainer = document.getElementById(id);
    messageContainer.scrollTop = messageContainer.scrollHeight;
}


function showToast(message) {
    const toast = document.createElement('div');
    toast.classList.add('toast-box');
    toast.innerHTML = message;

    let toastBox = document.getElementById('page-toast');
    toastBox.appendChild(toast);

    setTimeout(() => {
        toast.remove();
    }, 6000);

}

function storeToastMessage(message) {
    sessionStorage.setItem('toastMessage', message);
}

function showToastFromStorage() {
    const message = sessionStorage.getItem('toastMessage');
    if (message) {
        showToast(message);
        sessionStorage.removeItem('toastMessage'); 
    }
}
