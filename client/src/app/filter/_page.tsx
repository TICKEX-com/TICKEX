import axios, {AxiosResponse} from 'axios';

export const BASE_URL = "/api/event-service"

export const fetchByFilter = async (MinPrice:number,MaxPrice:number,eventType:string,city:string,PageNumber:number) => {
	let response : AxiosResponse;
	try {
		response =  await axios.get(`${BASE_URL}/Events/Filter?${eventType=="null"?null :`&EventType=${eventType}`}&MinPrice=${MinPrice??0}&MaxPrice=${MaxPrice??0}&pageNumber=${PageNumber??1}${city=="null"?null :`&City=${city}`}`)
		return response.data;
	} catch (error) {
		return null;
	}
};