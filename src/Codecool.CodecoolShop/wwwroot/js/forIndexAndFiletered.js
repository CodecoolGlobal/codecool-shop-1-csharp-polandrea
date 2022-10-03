let addButtons = document.querySelectorAll("#cart-button");
let numbers = document.querySelectorAll("#numbers");
let navCartButton = document.querySelector("#nav-cart-button");


for (let i of addButtons) {
    i.addEventListener('click', async() => {
        let productNumbers = await getNumberOfItemsInCart();
        apiPost("/api/AddToCart", i.dataset.id)
        productNumbers += 1;
        navCartButton.innerHTML = `Cart (${productNumbers})`
        addedToCart()
                        
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


function addedToCart() {
    alert("Item is added to Cart!");
}