import axios, {AxiosResponse} from 'axios';

export const BASE_URL = "api/event-service"

export const fetchEvents = async ( ) => {
	let response : AxiosResponse;
	try {
		response =  await axios.get(`${BASE_URL}/Events`)
		console.log(response.data)
		return response.data;
	} catch (error) {
		return null;
	}
};