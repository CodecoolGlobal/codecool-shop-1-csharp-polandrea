var addButtons = document.querySelectorAll("#cart-button");

for (let i of addButtons) {
    i.addEventListener('click', async () => {
        console.log(i)
        apiPost("/api/AddToCart", i.dataset.id)

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