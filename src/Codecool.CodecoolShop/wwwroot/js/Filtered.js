var addButtons = document.querySelectorAll("#cart-button");
var numbers = document.querySelectorAll("#numbers");
var navCartButton = document.querySelector("#nav-cart-button");


for (let i of addButtons) {
    i.addEventListener('click', async () => {
        var productNumbers = await getNumberOfItemsInCart();
        apiPost("/api/AddToCart", i.dataset.id)
        productNumbers += 1;
        navCartButton.innerHTML = `Cart (${productNumbers})`

    })
}



async function getNumberOfItemsInCart() {
    let response = fetch('/api/GetNumberOfItemsInCart')
    return (await response).json()
}




async function apiPost(url, payload) {
    let data = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(payload),
    })
    return await data.json()
}