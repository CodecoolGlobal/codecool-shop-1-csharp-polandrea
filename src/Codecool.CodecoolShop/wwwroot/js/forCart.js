var priceHolder = document.querySelector("#sum-price");
var removeButtons = document.querySelectorAll("#cart-remove-button");
var navCartButton = document.querySelector("#nav-cart-button");
var numbers = document.querySelectorAll("#numbers");


updateTotalPrice()


for (let i = 0; i < removeButtons.length; i++) {
    removeButtons[i].addEventListener('click', async () => {
        var productNumbers = await getNumberOfItemsInCart();
        productNumbers -= 1;

        apiPost("/api/DeleteFromCart", removeButtons[i].dataset.id)
        numbers[i].value -= 1;
        updateTotalPrice()
        if (numbers[i].value == 0) {
            var deleteProduct = document.querySelector(`#product-${removeButtons[i].dataset.id}`);
            deleteProduct.parentNode.removeChild(deleteProduct);
        }
        navCartButton.innerHTML = `Cart (${productNumbers})`
        

    })
}


async function getNumberOfItemsInCart() {
    let response = fetch('/api/GetNumberOfItemsInCart')
    return (await response).json()
}


async function updateTotalPrice() {
    currentPrice = await getItemsPrice()
    priceHolder.innerHTML = `Total price ${currentPrice}`
}


async function getItemsPrice() {
    let response = fetch('/api/GetItemsPrice')
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