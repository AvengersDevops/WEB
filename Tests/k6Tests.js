import http from 'k6/http'
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

export const options = {
    threshold: {
        http_req_failed: ['rate<0.01'],
        http_req_duration: ['p(95)<200']
    },
    duration: '5s',
    vus: 10
}

export default function () {
    const data = { id: 1 };
    http.post(`https://${__ENV.HOSTING}/task/read`, JSON.stringify(data),
        {
            headers: { 'Content-Type': 'application/json' }
        }
    );
}

export function handleSummary(data) {
    return {
        "summary.html": htmlReport(data),
    };
}