const domain = 'http://api.techwarriors.click/'
const routes = {
    GetAll: 'api/Article/GetList',
    post: 'api/Article/Insert',
    put: 'api/Article/Update',
    patch: 'api/Article/Delete',
    getDetails: 'api/Article/GetDetail',
}
const fetchData = async (url, method, callback, data) => {

    let response = await fetch(url, 
    {
        method: method ?? 'GET',
    },
    data ? JSON.stringify(data) : ''
    )
    response  = await response.json()
    callback ? callback(response.data) : ''
    console.log(response)
};

const GetListArticle = (data) => {
    let table = document.getElementById('table-news')
    let html = data.slice(0, 3).map((item, index) => {
        return `
            <div class="blog-item col-md-4 col-12 mb-30">
                <div class="image"><img src="${item.image}" alt="" height="250" style="object-fit: cover;" /></div>
                <div class="content">
                    <div class="meta">
                        <p class="date">10 JUN 2016</p>
                        <p class="cat"><a href="#">CRICKET</a></p>
                        <p class="author">BY <a href="#">ADMIN</a></p>
                    </div>
                    <h3 class="title">
                        <a href="#" class="text-overflow-1">${item.title}</a>
                    </h3>
                    <p class="text-overflow-5">
                        ${item.content}
                    </p>
                    <a href="#" class="read-more">READ MORE</a>
                </div>
            </div>
        `
    })
    if (table) {
        table.innerHTML = html.join('')
    }
}

// GetListArticle()
// http://api.techwarriors.click/api/Article/GetList?PageSize=10&CurrentPage=1
fetchData(`${domain + routes.GetAll}?PageSize=10&CurrentPage=1`, 'GET', GetListArticle)
