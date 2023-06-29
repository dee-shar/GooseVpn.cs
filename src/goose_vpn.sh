#!/bin/bash

api="https://api1.goosevpn.com"
user_agent="okhttp/4.9.0"

function sign_up() {
	# 1 - email: (string): <email>
	# 2 - password: (string): <password>
	response=$(curl --request POST \
		--url "$api/in_app/signup" \
		--user-agent "$user_agent" \
		--header "content-type: application/json" \
		--data '{
			"email": "'$1'",
			"first_name": "Goose",
			"last_name": "User",
			"password": "'$2'"
		}')
	if [ -n $(jq -r ".token" <<< "$response") ]; then
		token=$(jq -r ".token" <<< "$response")
	fi
	echo $response
}


function get_account_plan() {
	curl --request GET \
		--url "$api/users/me/plan" \
		--user-agent "$user_agent" \
		--header "content-type: application/json" \
		--header "authorization: Bearer: $token"
}

function get_account_info() {
	curl --request GET \
		--url "$api/users/me" \
		--user-agent "$user_agent" \
		--header "content-type: application/json" \
		--header "authorization: Bearer: $token"
}

function get_servers() {
	curl --request GET \
		--url "$api/users/me/servers" \
		--user-agent "$user_agent" \
		--header "content-type: application/json" \
		--header "authorization: Bearer: $token"
}
