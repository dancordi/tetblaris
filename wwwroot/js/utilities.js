//play audio
window.PlayAudio = (elementName) => {
    console.log("document", document);
    console.log("elementName", elementName);
    let elem = document.getElementById(elementName);
    console.log("elem", elem);
    elem.play();
}

//pause audio
window.PauseAudio = (elementName) => {
    document.getElementById(elementName).pause();
}

//set windows title
window.SetTitle = (title) => {
    document.title = title;
}

//set the focus to the element
window.SetFocusToElement = (element) => {
    element.focus();
};