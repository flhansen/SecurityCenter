#pragma once
#include <winsock.h>
#include <string>
#include <vector>
#include <map>
#include <stdexcept>
#include <thread>

namespace SecurityCenter::ScanEngine
{
	/// <summary>
	/// Scanner class to provide a decent amount of scanning techniques
	/// including port and ip scanning. This class takes care about different
	/// operating systems.
	/// </summary>
	class Scanner
	{
	private: /* Members */
		WSADATA m_WSAData;

	public: /* Constructors */
		Scanner();
		~Scanner();

	public: /* Public methods */
		/// <summary>
		/// Port scan, which uses TCP connections to determine open ports. This
		/// is not stealthy, because connections are established.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="startPort"></param>
		/// <param name="endPort"></param>
		/// <returns>Map of ports and their status</returns>
		std::map<int, bool> TcpConnectScan(const std::string& target, unsigned int startPort, unsigned int endPort);

		/// <summary>
		/// Stealthy port scan, which never completes a TCP connection while
		/// scanning. Sending SYN/SYN-ACK packages following to the TCP
		/// protocol. While under UNIX it is possible to use raw sockets to
		/// send and receive SYN/SYN-ACK packets, Windows restricts the access
		/// to raw sockets for security reasons. Therefore this method uses
		/// the ethernet interface to perform those scans.
		/// </summary>
		void TcpSynScan() const;

		/// <summary>
		/// Port scan using UDP packets to find running UDP services.
		/// </summary>
		void UdpScan() const;

	private: /* Helper methods */
		bool InitWinsock();
		void CleanupWinsock();
		bool CheckPortStatus(const std::string& ip, int port) const;
	};
}
