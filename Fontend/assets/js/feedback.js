const domain = 'http://api.techwarriors.click/'
const routes = {
    GetAll: 'api/Feedback/GetList',
    post: 'api/Feedback/Insert',
    // put: 'api/Feedback/Update',
    patch: 'api/Feedback/Delete',
    getDetails: 'api/Feedback/GetDetail',
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

const handleDelete = async (id) => {
    console.log(id);
    let url = domain + routes.patch + '?FeedbackId=' + id
    await fetch(url, {
        method: 'patch'
    })
    window.location.reload()
}

const GetListArticle = (data) => {
    let table = document.getElementById('table-article-list')
    let html = data.map((item, index) => {
        return `
            <tr>
                <th scope="row">${index + 1}</th>
                <td>${item.title}</td>
                <td>
                    <img src="${item.image}" alt="" width="50" height="50" class="rounded mx-auto">
                </td>
                <td>
                    ${validateDatetime(item.createDate)}
                </td>
                <td>
                    ${item.modifeDate}
                </td>
                <td>
                    <button type="button" class="btn btn-warning"
                        onclick="handleDelete('${item.articleID}')"
                    >
                        <i class="ti ti-trash"></i>
                    </button>
                    <button type="button" class="btn btn-primary"
                        onclick="handleUpdate('${item.articleID}')"
                    >
                        <i class="ti ti-settings"></i>
                    </button>
                </td>
            </tr>
        `
    })
    // console.log(table.hasChildNodes())
    // console.log(table.firstElementChild)
    // if (table) {
    //     // while (table.hasChildNodes()) {
    //     //     // table.removeChild(table.hasChildNodes());
    //     // }
    // }
    if (table) {
        table.innerHTML = html.join('')
    }
}


const validateDatetime = (datetimeString) => {
    // Kiểm tra định dạng ngày và giờ (datetime) hợp lệ
    // Định dạng mẫu: YYYY-MM-DDTHH:MM:SS.sss
  
    // var regex = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}\.\d{3}$/;
  
    // if (!regex.test(datetimeString)) {
    //   // Định dạng không hợp lệ
    //   return false;
    // }
  
    // Kiểm tra tính hợp lệ của ngày và giờ
    var datetime = new Date(datetimeString);
  
    if (isNaN(datetime.getTime())) {
      // Ngày và giờ không hợp lệ
      return false;
    }
    
    // Trích xuất thông tin ngày và giờ
    var month = datetime.getMonth() + 1; // Lấy tháng tính từ 0, nên cần cộng thêm 1
    var hours = datetime.getHours();
    var minutes = datetime.getMinutes();
    var day = datetime.getDate();
    var year = datetime.getFullYear();
  
    // Định dạng lại chuỗi ngày và giờ
    var formattedDatetime = `${padZero(month)}:${padZero(hours)} / ${padZero(day)}-${padZero(month)}-${year}`;
  
    // Trả về định dạng đã định kỳ
    return formattedDatetime;
}
const padZero = (number) => {
    return number.toString().padStart(2, '0');
}
// GetListArticle()
// http://api.techwarriors.click/api/Article/GetList?PageSize=10&CurrentPage=1
fetchData(`${domain + routes.GetAll}`, 'GET')

// const form = document.querySelector('#form-add-article')

// form?.addEventListener('submit', (e) => handleSubmit(e))

// const handleSubmit = async (e) => {
//     e.preventDefault()
//     const image = form.querySelector('#image').files[0]
//     const title = form.querySelector('#title').value
//     const content = form.querySelector('#content').value
//     console.log(title , content);
//     console.log(image);
//     let formImage = new FormData()
//     formImage.append('File', image, "IMAGE-TEST.JPG")
//     console.log(formImage);
//     let response = await fetch(`${domain}api/file/uploadfile`,
//         {
//             method: 'POST',
//             body: formImage
//         },
//     )
//     response = await response.json()
//     console.log('image', response);
//     let pathImage = response.object
//     console.log(pathImage);
//     let formData = new FormData()
//     formData.append("Title", JSON.stringify(title))
//     formData.append("Content", JSON.stringify(content))
//     // formData.append('Image', JSON.stringify(pathImage))
//     let data = JSON.stringify({
//         Title: title,
//         Content: content,
//         Description: 'desc',
//         Image: pathImage
//     })
//     response = await fetch(domain + routes.post, 
//         {
//             method: 'POST',
//             headers: { 'Content-Type': 'application/json' },
//             body: data
//         })
//     // window.location.href = '/article.html'
// }
