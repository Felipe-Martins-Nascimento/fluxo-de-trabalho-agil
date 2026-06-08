document.addEventListener("DOMContentLoaded", () => {

    const elements = document.querySelectorAll(".reveal");

    if (!elements.length) {
        return;
    }

    const observer = new IntersectionObserver(
        (entries, currentObserver) => {

            entries.forEach(entry => {

                if (!entry.isIntersecting) {
                    return;
                }

                entry.target.classList.add("active");

                currentObserver.unobserve(entry.target);
            });
        },
        {
            threshold: 0.1,
            rootMargin: "0px 0px -80px 0px"
        }
    );

    elements.forEach(element => {
        observer.observe(element);
    });

});