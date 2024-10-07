document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".addToCartBtn").forEach(function (button) {
        button.addEventListener("click", function (event) {
            event.preventDefault(); // Prevent form submission

            const productId = button.getAttribute("data-product-id");
            const quantity = 1; // You can change this based on user input

            fetch('@Url.Action("AddToCart", "Cart")', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'X-CSRF-TOKEN': document.querySelector('input[name="__RequestVerificationToken"]').value
                },
                body: JSON.stringify({ productId: productId, quantity: quantity })
            })
                .then(response => response.json())
                .then(data => {
                    const modalMessage = document.getElementById('cartModalMessage');

                    // Update modal content with response message
                    modalMessage.innerText = data.message;

                    // Initialize and show the modal
                    const cartModal = new bootstrap.Modal(document.getElementById('cartModal'));
                    cartModal.show();
                })
                .catch(error => {
                    console.error('Error:', error.message);
                    alert('An error occurred while adding the product to the cart.');
                });
        });
    });
});