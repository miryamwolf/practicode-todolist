import axios from "axios";

const apiUrl = process.env.REACT_APP_API_URL;
axios.defaults.baseURL = process.env.REACT_APP_API_URL;
axios.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded';
axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';

//תפיסת שגיאות
axios.interceptors.response.use(function (response) {
  // Any status code that lie within the range of 2xx cause this function to trigger
  // Do something with response data
  return response;
}, function (error) {
  // Any status codes that falls outside the range of 2xx cause this function to trigger
  // Do something with response error
  return Promise.reject(error);
});
export default {

  getTasks: async () => {
    const result = await axios.get(`${apiUrl}/tasks`)    
    return result.data;
  },


  addTask: async(item)=>{
    
    console.log('addTask', item)
    const r=await axios.post(`${apiUrl}/add`,item,{
    headers: {
        'Content-Type': 'application/json'
    }})
return r.data;
},

  setCompleted: async(id, isComplete)=>{
    // console.log('setCompleted', {id, isComplete})
await axios.put(`${apiUrl}/put/${id}`,{isComplete},{
      headers: {
        'Content-Type': 'application/json'
    }});
    
    return {};
  },

  deleteTask:async(id)=>{
    console.log('deleteTask')
   await axios.delete(`${apiUrl}/remove/${id}`)
    return {};
  }

};
