#include "Scanner.h"

namespace SecurityCenter::ScanEngine
{
	Scanner::Scanner()
	{
		if (!InitWinsock())
			throw std::exception("Could not initialize winsock.");
	}

	Scanner::~Scanner()
	{
		CleanupWinsock();
	}

	std::map<int, bool> Scanner::TcpConnectScan(const std::string& target, unsigned int startPort, unsigned int endPort)
	{
		std::map<int, bool> portStatusMap;
		std::vector<std::thread> thread_pool;

		for (unsigned int port = startPort; port <= endPort; port++)
		{
			std::thread thread([this, &target, &port, &portStatusMap]()
			{
				bool isPortOpen = CheckPortStatus(target, port);
				portStatusMap[port] = isPortOpen;
			});

			thread_pool.emplace_back(thread);
		}

		// Wait for all threads to be finished
		for (std::thread& thread : thread_pool)
			thread.join();

		return portStatusMap;
	}

	void Scanner::TcpSynScan() const
	{
		// TODO: Implement ethernet interface to scan open ports using TCP SYN/SYN-ACK packets.
	}

	void Scanner::UdpScan() const
	{
		// TODO: Implement UDP connections to scan open ports.
	}

	bool Scanner::InitWinsock()
	{
		int err = WSAStartup(MAKEWORD(2, 2), &m_WSAData);

		if (err)
			return false;

		// Check if the version is correct
		if (m_WSAData.wVersion != MAKEWORD(2, 2))
		{
			WSACleanup();
			return false;
		}

		return true;
	}

	void Scanner::CleanupWinsock()
	{
		WSACleanup();
	}

	bool Scanner::CheckPortStatus(const std::string& ip, int port) const
	{
		SOCKADDR_IN target;
		target.sin_family = AF_INET;					// Use IPv4
		target.sin_port = htons(port);					// Reformat port bytes
		target.sin_addr.s_addr = inet_addr(ip.c_str()); // Parse the ip string

		// Create the TCP socket
		SOCKET sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

		if (sock == INVALID_SOCKET)
			return false;

		// Connect with target endpoint
		bool connected = connect(sock, (SOCKADDR*)&target, sizeof(target)) != SOCKET_ERROR;

		if (connected)
			closesocket(sock);

		// True, when connection established
		return connected;
	}
}