var addButtons = document.querySelectorAll("#cart-button");
var removeButtons = document.querySelectorAll("#cart-remove-button");
var numbers = document.querySelectorAll("#numbers");


for (let i of addButtons) {
    i.addEventListener('click', async() => {
        console.log(i)
        apiPost("/api/AddToCart", i.dataset.id)

    })
}


for (let i = 0; i < removeButtons.length; i++) {
    removeButtons[i].addEventListener('click', async () => {
        console.log(numbers[i])
        apiPost("/api/DeleteFromCart", removeButtons[i].dataset.id)
        numbers[i].value -= 1;
        if (numbers[i].value == 0) {
            var deleteProduct = document.querySelector(`#product-${removeButtons[i].dataset.id}`);
            deleteProduct.parentNode.removeChild(deleteProduct);
        }

    })
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
