let actualUri = '/';

function manipulateActiveLink(firstRender, uri) {

    if (!firstRender)
        return;

    actualUri = '/' + uri;
    let activeAnchor = document.querySelector("[href='" + actualUri + "']");
    if (activeAnchor === null) {
        activeAnchor = document.querySelector('a.active');
        //activeAnchor = document.querySelector("span.k-menu-link > a.active");
    }

    activeAnchor.parentElement.classList.add('bg_active');

    let anagraficaLiElement = document.querySelector("[aria-haspopup=true]");

    anagraficaLiElement.addEventListener('load', e => {
        OnClickHandler({ url: actualUri });
    });
}

function OnClickHandler(item) {
    actualUri = item.url;

    let spans = document.querySelectorAll('span.bg_active');
    spans.forEach((span) => {
        span.classList.remove('bg_active');
    });

    var linkAttivo = document.querySelector('.active');

    var nodoSpan = linkAttivo.closest(".k-menu-link");
    var nodoLI = linkAttivo.closest(".k-menu-item");
    
    if (item !== null) {
        let activeAnchor = document.querySelector("[href='" + item.url + "']");
        activeAnchor.parentElement.classList.add("bg_active");

        if (activeAnchor.parentElement.parentElement.parentElement.parentElement.classList.contains('k-menu-popup')) {
            document.querySelector("[aria-haspopup=true]").firstChild.classList.add("bg_active_padre");
            nodoLI.classList.add("bg_active");
            document.querySelector("[aria-haspopup=true]").setAttribute('aria-hasopenmenu', true);
        } else if (!item.ModifyOnClick) {
            nodoLI.classList.remove("bg_active");
        } else {
            document.querySelector("[aria-hasopenmenu=true]").setAttribute('aria-hasopenmenu', false);
        }
    }
}