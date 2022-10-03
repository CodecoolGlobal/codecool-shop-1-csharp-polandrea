let priceHolder = document.querySelector("#sum-price");
let removeButtons = document.querySelectorAll("#cart-remove-button");
let navCartButton = document.querySelector("#nav-cart-button");
let numbers = document.querySelectorAll("#numbers");


updateTotalPrice()


for (let i = 0; i < removeButtons.length; i++) {
    removeButtons[i].addEventListener('click', async () => {
        let productNumbers = await getNumberOfItemsInCart();
        productNumbers -= 1;

        apiPost("/api/DeleteFromCart", removeButtons[i].dataset.id)
        numbers[i].value -= 1;
        updateTotalPrice()
        if (numbers[i].value == 0) {
            let deleteProduct = document.querySelector(`#product-${removeButtons[i].dataset.id}`);
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
    priceHolder.innerHTML = `Total price:  ${currentPrice}`
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